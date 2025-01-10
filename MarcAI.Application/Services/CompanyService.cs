using MarcAI.Application.Dtos.Companies;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;
using MarcAI.Domain.Models.ValueObjects;

namespace MarcAI.Application.Services;

internal class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public Task<IEnumerable<Company>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Company?> GetById(Guid id)
        => await _companyRepository.GetById(id);

    public async Task<Company> Create(RegisterCompanyDto data)
    {
        if(await _companyRepository.ExistsAsync(c => c.CorporateName == data.CorporateName))
            throw new ArgumentException("Corporate name already exists.");

        var validCnpj = Cnpj.Create(data.Cnpj!.Value);

        var validAddress = Address.Create(data.Address!.Street,
                                    data.Address!.Number,
                                    data.Address!.Complement,
                                    data.Address!.Neighborhood,
                                    data.Address!.City,
                                    data.Address.State,
                                    data.Address!.ZipCode);

        var newComapany = Company.Create(data.CorporateName!, data.FantasyName!, validAddress, validCnpj);

        return await _companyRepository.Create(newComapany);
    }
    public async Task<Company> Update(UpdateCompanyDto data)
    {
        var company = await _companyRepository.GetByIdToUpdate(data.Id!.Value) 
            ?? throw new ArgumentException("Company not found.");

        company.Update(data.FantasyName, data.Address);

        return await _companyRepository.Update(company);
    }

    public async Task<bool> Delete(Guid id)
    {
        var company = await _companyRepository.GetByIdToUpdate(id) ??
                 throw new ArgumentException("Company not found.");

        company.Delete();

        await _companyRepository.Update(company);

        return true;
    }

}
