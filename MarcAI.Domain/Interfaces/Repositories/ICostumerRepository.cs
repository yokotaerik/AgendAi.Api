﻿using MarcAI.Domain.Models.Entities;

namespace MarcAI.Domain.Interfaces.Repositories;

public interface ICostumerRepository : IUnitOfWork
{
    Task<Customer?> GetByIdAsync(Guid id);
    Task Add(Customer costumer);
}
