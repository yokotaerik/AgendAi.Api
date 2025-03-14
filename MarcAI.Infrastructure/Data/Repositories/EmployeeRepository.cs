using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;
using MarcAI.Infrastructure.Data.Context;
using MarcAI.Infrastructure.Data.UoW;
using Microsoft.EntityFrameworkCore;

namespace MarcAI.Infrastructure.Data.Repositories
{
    internal class EmployeeRepository : UnitOfWork, IEmployeeRepository
    {
        private DbSet<Employee> _dbSet;

        public EmployeeRepository(AppDbContext context) : base(context)
        {
            _dbSet = _context.Set<Employee>();
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            await _dbSet.AddAsync(employee);
            return employee;
        }

        public async Task<Employee?> GetByCompanyIdAsync(Guid companyId)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.CompanyId == companyId);
        }

        public async Task<Employee?> GetByCpfAsync(string cpf)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Cpf.Value == cpf);
        }

        public async Task<Employee?> GetByEmailAsync(string email)
        {
            return await _dbSet.Include(e => e.Company).FirstOrDefaultAsync(x => x.User.Email == email);
        }

        public Task<Employee?> GetByIdAsync(Guid id)
        {
            return _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Employee?> GetByUserIdAsync(Guid userId)
        {
            return _dbSet.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public Task<Employee> UpdateAsync(Employee employee)
        {
            _dbSet.Update(employee);
            return Task.FromResult(employee);
        }
    }
}
