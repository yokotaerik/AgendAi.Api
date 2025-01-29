using MarcAI.Domain.Interfaces;
using MarcAI.Infrastructure.Data.Context;

namespace MarcAI.Infrastructure.Data.UoW
{
    internal class UnitOfWork : IUnitOfWork
    {
        protected readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
            => await _context.SaveChangesAsync() > 0;

        public Task Roolback()
            => Task.CompletedTask;
    }
}
