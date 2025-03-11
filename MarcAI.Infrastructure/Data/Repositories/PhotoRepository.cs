using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;
using MarcAI.Infrastructure.Data.Context;
using MarcAI.Infrastructure.Data.UoW;
using Microsoft.EntityFrameworkCore;

namespace MarcAI.Infrastructure.Data.Repositories;

internal class PhotoRepository(AppDbContext context) : UnitOfWork(context), IPhotoRepository
{
    private readonly DbSet<Photo> _dbSet = context.Set<Photo>();

    public void Add(Photo photo)
    {
        _dbSet.Add(photo);
    }

    public async Task<Photo?> GetAsync(Guid id)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.Id == id);
    }

    public void Remove(Photo photo) => _dbSet.Remove(photo);
}
