using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BaseServiceApiRest_Core.DTOs;
using BaseServiceApiRest.Controllers.Base;
using BaseServiceApiRest_Core.Entities;
using BaseServiceApiRest_Core.Interfaces.IServices;

namespace BaseServiceApiRest.Controllers;

[ApiController]
[Route("[controller]")]
public class HelpFriendController : BaseController<HelpFriendDTO, IHelpFriendService>
{
    public HelpFriendController(ILogger<HelpFriend> logger, IMapper mapper, IHelpFriendService service) : base(logger, mapper, service)
    { }
}