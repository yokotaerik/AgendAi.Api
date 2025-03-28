﻿using AutoMapper;
using MarcAI.Application.Dtos.Common.User;
using MarcAI.Application.Dtos.Companies;
using MarcAI.Application.Dtos.Services;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Interfaces;
using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;
using MarcAI.Domain.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace MarcAI.Application.Services;

internal class CompanyService : ICompanyService
{
    private readonly IEmployeeService _employeeService;
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;
    private readonly IUserManager _userManager;

    public CompanyService(ICompanyRepository companyRepository, IMapper mapper, IEmployeeService employeeService, IUserManager userManager)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
        _employeeService = employeeService;
        _userManager = userManager;
    }

    public async Task<IEnumerable<CompanyDto>> GetList() => _mapper.Map<IEnumerable<CompanyDto>>(await _companyRepository.GetList());

    public async Task<CompleteCompanyDto?> GetById(Guid id)
    {
        var queryable = _companyRepository.GetCompanyQueryable();

        var companyDto = await queryable
          .Where(c => c.Id == id)
          .Select(c => new CompleteCompanyDto
          {
              Id = c.Id,
              CorporateName = c.CorporateName,
              FantasyName = c.FantasyName,
              Address = c.Address,
              ImageUrls = c.Photos.Select(p => p.WebUrl).ToList(),
              Employees = c.Employees.Select(e => new BasicInfoDto(e.Id, e.Photo != null ? e.Photo.WebUrl : "", e.User.Name + " " + e.User.Surname)).ToList(),
              Services = c.Services.Select(s => _mapper.Map<ServiceDto>(s)).ToList()
          })
          .FirstOrDefaultAsync();

        return companyDto;
    }

    public async Task<Guid?> GetOwnerUserId(Guid companyId)
    {
        return await _companyRepository.GetOwnerUserId(companyId);
    }


    public async Task Create(RegisterCompanyDto data)
    {
        if(await _companyRepository.ExistsAsync(c => c.CorporateName == data.CorporateName))
            throw new ArgumentException("Corporate name already exists.");

        var validAddress = Address.Create(data.Address!.Street,
                                    data.Address!.Number,
                                    data.Address!.Complement,
                                    data.Address!.Neighborhood,
                                    data.Address!.City,
                                    data.Address.State,
                                    data.Address!.ZipCode);

        var newComapany = Company.Create(data.CorporateName!, data.FantasyName!, validAddress);

        await _companyRepository.Create(newComapany);

        var employee = await _employeeService.Create(data.Owner, newComapany.Id);

        newComapany.AddEmployee(employee);

        await _userManager.CreateUserAsync(employee.User, data.Owner.Password);
    }

    public async Task Update(UpdateCompanyDto data)
    {
        var company = await _companyRepository.GetByIdToUpdate(data.Id!.Value) 
            ?? throw new ArgumentException("Company not found.");

        var addressDto = data.Address;

        var address = Address.Create(addressDto.Street,
                                     addressDto.Number,
                                     addressDto.Complement,
                                     addressDto.Neighborhood,
                                     addressDto.City,
                                     addressDto.State,
                                     addressDto.ZipCode);

        company.Update(data.FantasyName, address);

         _companyRepository.Update(company);

        if (!await _companyRepository.Commit()) throw new InvalidOperationException("Failed to persist");
    }

    public async Task<bool> Delete(Guid id)
    {
        var company = await _companyRepository.GetByIdToUpdate(id) ??
                 throw new ArgumentException("Company not found.");

        company.Delete();

        _companyRepository.Update(company);

        if (!await _companyRepository.Commit()) throw new InvalidOperationException("Failed to persist");

        return true;
    }


    #region METHODS ANDS FUNCTIONS
    
    private static void HandlePhoneData(Company company, List<string> phones)
    {
        var phonesToRemove = company.Contacts
            .Where(c => !phones.Contains(c.Info))
            .ToList();

        foreach (var phone in phonesToRemove)
            company.RemoveContact(phone);

        var existingPhones = company.Contacts.Select(p => p.Info).ToList();
        var newPhones = phones.Where(p => !existingPhones.Contains(p)).ToList();

        foreach (var phone in newPhones)
            company.AddContact(ContactInfo.CreatePhone(phone));
    }


    #endregion
}
