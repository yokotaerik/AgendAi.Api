using MarcAI.Application.Dtos.Companies;
using MarcAI.Domain.Models.Entities;

namespace MarcAI.Application.Interfaces;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetList();
    Task<CompleteCompanyDto?> GetById(Guid id);
    Task<CompanyDto> Create(RegisterCompanyDto data);
    Task<CompanyDto> Update(UpdateCompanyDto data);
    Task<bool> Delete(Guid id);
}
