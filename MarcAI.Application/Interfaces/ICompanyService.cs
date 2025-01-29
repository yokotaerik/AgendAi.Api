using MarcAI.Application.Dtos.Companies;
using MarcAI.Domain.Models.Entities;

namespace MarcAI.Application.Interfaces;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetAll();
    Task<CompanyDto?> GetById(Guid id);
    Task<CompanyDto> Create(RegisterCompanyDto data);
    Task<CompanyDto> Update(UpdateCompanyDto data);
    Task<bool> Delete(Guid id);
}
