using MarcAI.Application.Dtos.Companies;
using MarcAI.Domain.Models.Entities;

namespace MarcAI.Application.Interfaces;

public interface ICompanyService
{
    Task<IEnumerable<Company>> GetAll();
    Task<Company?> GetById(Guid id);
    Task<Company> Create(RegisterCompanyDto data);
    Task<Company> Update(UpdateCompanyDto data);
    Task<bool> Delete(Guid id);
}
