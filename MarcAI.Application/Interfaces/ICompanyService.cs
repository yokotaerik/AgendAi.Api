using MarcAI.Application.Dtos.Companies;

namespace MarcAI.Application.Interfaces;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetList();
    Task<CompleteCompanyDto?> GetById(Guid id);
    Task<Guid?> GetOwnerUserId(Guid companyId);
    Task Create(RegisterCompanyDto data);
    Task Update(UpdateCompanyDto data);
    Task<bool> Delete(Guid id);
}
