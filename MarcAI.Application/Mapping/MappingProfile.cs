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
        CreateMap<Company, CompanyDto>()
            .ForMember(dto => dto.ImageUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault()!.WebUrl ?? ""))
            .ReverseMap();
        CreateMap<Company, CompleteCompanyDto>()
            .ForMember(dto => dto.Employees, opt => opt.MapFrom(src => src.Employees))
            .ForMember(dto => dto.Services, opt => opt.MapFrom(src => src.Services))
            .ForMember(dto => dto.ImageUrls, opt => opt.MapFrom(src => src.Photos.Select(p => p.WebUrl)))
            .ReverseMap();

        //Services 
        CreateMap<Service, ServiceDto>().ReverseMap();

        //Employee
        CreateMap<Employee, EmployeeDto>()
            .ForMember(dto => dto.ImageUrl, opt => opt.MapFrom(src => src.Photo != null ? src.Photo.WebUrl : ""))
            .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dto => dto.Surname, opt => opt.MapFrom(src => src.User.Surname))
            .ForMember(dto => dto.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dto => dto.Company, opt => opt.MapFrom(src => src.Company))
            .ForMember(dto => dto.Services, opt => opt.MapFrom(src => src.OfferedServices))
            .ReverseMap();
    }
}
