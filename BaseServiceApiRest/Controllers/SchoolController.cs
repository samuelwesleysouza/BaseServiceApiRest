using AutoMapper;
using BaseServiceApiRest.Controllers.Base;
using BaseServiceApiRest_Core.DTOs;
using BaseServiceApiRest_Core.Entities;
using BaseServiceApiRest_Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace DigitalElections.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SchoolController : BaseController<SchoolDTO, ISchoolService>
{
    public SchoolController(ILogger<School> logger, IMapper mapper, ISchoolService service) : base(logger, mapper, service)
    { }
}