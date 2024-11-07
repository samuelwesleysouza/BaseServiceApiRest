using BaseServiceApiRest_Core.DTOs;
using BaseServiceApiRest_Core.DTOs.ResponseDTOs;
namespace BaseServiceApiRest_Core.Interfaces.IServices;

public interface IAuthenticationService
{
    Task<LoginResponseDTO> Login(LoginDTO dto);  // Método para autenticar um usuário
    Task<LoginResponseDTO> LoginWithManagerAccount(LoginDTO dto);  // Método para autenticar um gerente
}
