using AutoMapper;
using MarcAI.Application.Dtos.Companies;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;
using MarcAI.Domain.Models.ValueObjects;

namespace MarcAI.Application.Services;

internal class CompanyService : ICompanyService
{
    private readonly IEmployeeService _employeeService;
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public CompanyService(ICompanyRepository companyRepository, IMapper mapper, IEmployeeService employeeService)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
        _employeeService = employeeService;
    }

    public async Task<IEnumerable<CompanyDto>> GetList() => _mapper.Map<IEnumerable<CompanyDto>>(await _companyRepository.GetList());

    public async Task<CompleteCompanyDto?> GetById(Guid id)
        => _mapper.Map<CompleteCompanyDto>(await _companyRepository.GetById(id));

    public async Task<CompanyDto> Create(RegisterCompanyDto data)
    {
        if(await _companyRepository.ExistsAsync(c => c.CorporateName == data.CorporateName))
            throw new ArgumentException("Corporate name already exists.");

        var validCnpj = Cnpj.Create(data.Cnpj!);

        var validAddress = Address.Create(data.Address!.Street,
                                    data.Address!.Number,
                                    data.Address!.Complement,
                                    data.Address!.Neighborhood,
                                    data.Address!.City,
                                    data.Address.State,
                                    data.Address!.ZipCode);

        var newComapany = Company.Create(data.CorporateName!, data.FantasyName!, validAddress, validCnpj);

        await _companyRepository.Create(newComapany);

        var employee = await _employeeService.Create(data.Owner, newComapany.Id);

        newComapany.AddEmployee(employee);

        if (!await _companyRepository.Commit()) throw new InvalidOperationException("Failed to persist");

        return _mapper.Map<CompanyDto>(newComapany);
    }

    public async Task<CompanyDto> Update(UpdateCompanyDto data)
    {
        var company = await _companyRepository.GetByIdToUpdate(data.Id!.Value) 
            ?? throw new ArgumentException("Company not found.");

        company.Update(data.FantasyName, data.Address);

         _companyRepository.Update(company);

        if (!await _companyRepository.Commit()) throw new InvalidOperationException("Failed to persist");

        return _mapper.Map<CompanyDto>(company);
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
}
