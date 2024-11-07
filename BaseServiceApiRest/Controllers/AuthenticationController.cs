using BaseServiceApiRest_Core.DTOs;
using BaseServiceApiRest_Core.DTOs.ResponseDTOs;
using BaseServiceApiRest_Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BaseServiceApiRest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> UserLogin([FromBody] LoginDTO login)
        {
            var result = await _authenticationService.Login(login);
            if (result is null)
            {
                return NotFound("User not found or invalid credentials.");
            }

            return Ok(result); // Retorna o token e os dados do usuário
        }

        [HttpPost("manager-login")]
        public async Task<ActionResult<LoginResponseDTO>> ManagerLogin([FromBody] LoginDTO login)
        {
            var result = await _authenticationService.LoginWithManagerAccount(login);
            if (result is null)
            {
                return NotFound("Manager not found or invalid credentials.");
            }

            return Ok(result); // Retorna o token e os dados do gerente
        }
    }
}
