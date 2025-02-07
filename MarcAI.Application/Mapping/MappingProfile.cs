using AutoMapper;
using MarcAI.Application.Dtos.Companies;
using MarcAI.Application.Dtos.Costumers;
using MarcAI.Application.Dtos.Employees;
using MarcAI.Application.Dtos.Services;
using MarcAI.Domain.Models.Entities;

namespace MarcAI.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Costumer
        CreateMap<Customer, CostumerDto>().ReverseMap();

        // Comapany Dtos
        CreateMap<Company, CompanyDto>().ReverseMap();
        CreateMap<Company, CompleteCompanyDto>()
            .ForMember(dto => dto.Employees, opt => opt.MapFrom(src => src.Employees))
            .ForMember(dto => dto.Services, opt => opt.MapFrom(src => src.Services))
            .ReverseMap();

        //Services 
        CreateMap<Service, ServiceDto>().ReverseMap();

        //Employee
        CreateMap<Employee, EmployeeDto>().ReverseMap();
    }
}
