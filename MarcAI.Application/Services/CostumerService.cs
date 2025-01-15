using AutoMapper;
using MarcAI.Application.Dtos.Costumers;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;

namespace MarcAI.Application.Services;

internal class CostumerService : ICustomerService
{
    private readonly ICostumerRepository _costumerRepository;
    private readonly IMapper _mapper;

    public CostumerService(ICostumerRepository costumerRepository, IMapper mapper)
    {
        _costumerRepository = costumerRepository;
        _mapper = mapper;
    }

    public Task<CostumerDto> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CostumerDto>> GetList()
    {
        throw new NotImplementedException();
    }

    public async Task<CostumerDto> Create(RegisterCostumerDto data)
    {
        var newUserToCostumer = User.Create(data.Credentials.Email, data.Credentials.Password);

        var newCostumer = Customer.Create(data.Name!, data.Surname!, data.Cpf!, newUserToCostumer.Id);

        await _costumerRepository.Add(newCostumer);
    
        return _mapper.Map<CostumerDto>(newCostumer);
    }

    public Task<CostumerDto> Update(UpdateCostumerDto data)
    {
        throw new NotImplementedException();
    }

    public Task<CostumerDto> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
