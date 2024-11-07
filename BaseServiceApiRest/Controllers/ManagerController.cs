using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BaseServiceApiRest.Controllers.Base;
using BaseServiceApiRest_Core.DTOs;
using BaseServiceApiRest_Core.Interfaces.IServices;
using BaseServiceApiRest_Core.Entities;

namespace DigitalElections.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ManagerController : BaseController<ManagerDTO, IManagerService>
{
    public ManagerController(ILogger<Manager> logger, IMapper mapper, IManagerService service) : base(logger, mapper, service)
    { }

    [HttpPost]
    [AllowAnonymous]
    public override async Task<ActionResult<ManagerDTO>> Post([FromBody] ManagerDTO dto)
    {
        var result = await _service.Create(dto);

        if (result is null) return NotFound();

        return Ok(result);
    }

    [HttpGet("leaders")]
    public async Task<ActionResult<List<UserDTO>>> GetLeaders()
    {
        var result = await _service.GetLeaders();

        return Ok(result);
    }
}
