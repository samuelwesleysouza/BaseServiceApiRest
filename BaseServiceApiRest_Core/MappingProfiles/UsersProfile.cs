using AutoMapper;
using BaseServiceApiRest_Core.DTOs;
using BaseServiceApiRest_Core.DTOs.ResponseDTOs;
using BaseServiceApiRest_Core.Entities;


namespace BaseServiceApiRest_Core.MappingProfiles;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<UserDTO, Users>().ReverseMap();
        CreateMap<Users, LoginData>();
    }
}