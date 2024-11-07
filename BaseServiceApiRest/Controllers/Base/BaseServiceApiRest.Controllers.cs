using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BaseServiceApiRest.Controllers;

[ApiController]
[Route("[controller]")]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult HandleError()
    {
        var problem = new ProblemDetails
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Title = "An error occurred",
            Detail = "An unexpected error occurred while processing your request."
        };

        return StatusCode((int)HttpStatusCode.InternalServerError, problem);
    }
}