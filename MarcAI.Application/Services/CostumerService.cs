using AutoMapper;
using MarcAI.Application.Dtos.Costumers;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Interfaces;
using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;

namespace MarcAI.Application.Services;

internal class CostumerService : ICustomerService
{
    private readonly ICostumerRepository _costumerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUserManager _userManager;

    public CostumerService(ICostumerRepository costumerRepository, IMapper mapper, IUserRepository userRepository, IUserManager userManager)
    {
        _costumerRepository = costumerRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _userManager = userManager;
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
        var newUserToCostumer = User.Create(data.Name!, data.Surname!, data.Credentials.Email, Domain.Enums.User.UserType.Customer);

        await _userRepository.Create(newUserToCostumer, data.Credentials.Password);

        var newCostumer = Customer.Create(newUserToCostumer.Id);

        await _costumerRepository.Add(newCostumer);

        await _userManager.CreateUserAsync(newUserToCostumer, data.Credentials.Password);

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
