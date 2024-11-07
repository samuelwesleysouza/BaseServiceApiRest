using AutoMapper;
using BaseServiceApiRest_Core.DTOs;
using BaseServiceApiRest_Core.Entities;


namespace BaseServiceApiRest_Core.MappingProfiles;

public class SchoolProfile : Profile
{
    public SchoolProfile()
    {
        CreateMap<School, SchoolDTO>().ReverseMap();
    }
}