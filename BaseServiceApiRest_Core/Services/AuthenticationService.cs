using BaseServiceApiRest_Core.DTOs;
using BaseServiceApiRest_Core.Interfaces.Repositories; // Aqui você vai importar as interfaces dos repositórios de usuário e gerente
using System.Net;
using System.Threading.Tasks;
using BaseServiceApiRest_Core.Utils;
using BaseServiceApiRest_Core.DTOs.ResponseDTOs;
using AutoMapper;
using BaseServiceApiRest_Core.Entities;
using BaseServiceApiRest_Core.Interfaces.IServices;
using BaseServiceApiRest_Core.Interfaces.IRepositories;
namespace BaseServiceApiRest_Core.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtProviderService _jwtProviderService;
    private readonly IUserRepository _userRepository;
    private readonly IManagerRepository _managerRepository;
    private readonly IMapper _mapper;

    public AuthenticationService(IJwtProviderService jwtProviderService,
                                 IUserRepository userRepository,
                                 IMapper mapper,
                                 IManagerRepository managerRepository)
    {
        _jwtProviderService = jwtProviderService;
        _userRepository = userRepository;
        _mapper = mapper;
        _managerRepository = managerRepository;
    }

    public async Task<LoginResponseDTO> Login(LoginDTO dto)
    {
        var user = await _userRepository.UserEmail(dto.Email);

        if (user is null)
        {
            throw new HttpRequestException("User not found", null, HttpStatusCode.NotFound);
        }

        var correctPassword = Hashing.VerifyArgon2(user.Password, dto.Password);

        if (!correctPassword)
        {
            throw new HttpRequestException("Password not correct", null, HttpStatusCode.NotFound);
        }

        var token = _jwtProviderService.GenerateToken(user.UserType, user.Id);

        return new LoginResponseDTO(token, _mapper.Map<Users, LoginData>(user));
    }

    public async Task<LoginResponseDTO> LoginWithManagerAccount(LoginDTO dto)
    {
        var managerAccount = await _managerRepository.SearchOne(x => x.Email == dto.Email);

        if (managerAccount is null)
        {
            throw new HttpRequestException("Manager not found", null, HttpStatusCode.NotFound);
        }

        var correctPassword = Hashing.VerifyArgon2(managerAccount.Password, dto.Password);

        if (!correctPassword)
            throw new HttpRequestException("User not found", null, HttpStatusCode.NotFound);

        var token = _jwtProviderService.GenerateToken(managerAccount.Role, managerAccount.Id);

        return new LoginResponseDTO(token, _mapper.Map<Manager, LoginData>(managerAccount));
    }
}