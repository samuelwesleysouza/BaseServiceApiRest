using AutoMapper;
using BaseServiceApiRest_Core.DTOs;
using BaseServiceApiRest_Core.DTOs.ResponseDTOs;
using BaseServiceApiRest_Core.Entities;


namespace BaseServiceApiRest_Core.MappingProfiles;

public class ManagerProfile : Profile
{
    public ManagerProfile()
    {
        CreateMap<Manager, ManagerDTO>().ReverseMap();
        CreateMap<Manager, LoginData>();
    }
}