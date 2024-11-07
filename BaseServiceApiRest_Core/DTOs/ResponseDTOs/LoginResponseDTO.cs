using BaseServiceApiRest_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseServiceApiRest_Core.DTOs.ResponseDTOs;

public class LoginResponseDTO
{
    public LoginResponseDTO(string token, LoginData data)
    {
        Token = token;
        Data = data;
    }

    public string Token { get; set; }
    public LoginData Data { get; set; }
}

public class LoginData
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public UserTypeEnum UserType { get; set; }
}