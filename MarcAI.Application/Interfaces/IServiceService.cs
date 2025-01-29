﻿using MarcAI.Application.Dtos.Services;
using MarcAI.Domain.Models.Entities;

namespace MarcAI.Application.Interfaces;

public interface IServiceService
{
    Task<IEnumerable<ServiceDto>> GetAll();
    Task<ServiceDto> GetById(Guid id);
    Task<ServiceDto> Create(RegisterServiceDto service);
    Task<ServiceDto> Update(UpdateServiceDto service);
    Task<bool> Delete(Guid id);
}
