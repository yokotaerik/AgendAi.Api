using AutoMapper;
using MarcAI.Application.Dtos.Companies;
using MarcAI.Application.Dtos.Costumers;
using MarcAI.Application.Dtos.Services;
using MarcAI.Domain.Models.Entities;

namespace MarcAI.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CostumerDto>().ReverseMap();

        CreateMap<Company, CompanyDto>().ReverseMap();

        CreateMap<Service, ServiceDto>().ReverseMap();
    }
}
