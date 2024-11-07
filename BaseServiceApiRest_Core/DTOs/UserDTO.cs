using BaseServiceApiRest_Core.Entities;
using Microsoft.AspNetCore.Http;

namespace BaseServiceApiRest_Core.DTOs;

public class UserDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Photo { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public long? ManagerId { get; set; } 
    public UserTypeEnum UserType { get; set; }
}