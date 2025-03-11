using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;
using MarcAI.Infrastructure.Data.Context;
using MarcAI.Infrastructure.Data.UoW;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MarcAI.Infrastructure.Data.Repositories;

internal class PhotoRepository(AppDbContext context) : UnitOfWork(context), IPhotoRepository
{
    private readonly DbSet<Photo> _dbSet = context.Set<Photo>();

    public void Add(Photo photo)
    {
        _dbSet.Add(photo);
    }
}
