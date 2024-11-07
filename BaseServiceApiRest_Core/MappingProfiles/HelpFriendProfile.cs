using AutoMapper;
using BaseServiceApiRest_Core.DTOs;
using BaseServiceApiRest_Core.Entities;


namespace BaseServiceApiRest_Core.MappingProfiles;

public class HelpFriendProfile : Profile
{
    public HelpFriendProfile()
    {
        CreateMap<HelpFriend, HelpFriendDTO>().ReverseMap();
    }
}