using AutoMapper;
using BaseServiceApiRest.Controllers.Base;
using BaseServiceApiRest_Core.DTOs;
using BaseServiceApiRest_Core.Entities;
using BaseServiceApiRest_Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace DigitalElections.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : BaseController<PersonDTO, IPersonService>
{
    public PersonController(ILogger<Person> logger, IMapper mapper, IPersonService service) : base(logger, mapper, service)
    { }

    [HttpGet("neighborhood-person")]
    public async Task<ActionResult<List<PersonNeighborhoodDTO>>> GetLeaders()
    {
        var result = await _service.GetPersonByNeighborhood();

        return Ok(result);
    }

    [HttpGet("transfer-voters")]
    public async Task<ActionResult<List<Person>>> GetTransferVoters()
    {
        var result = await _service.GetTransferVoters();

        return Ok(result);
    }
}