using BaseServiceApiRest_Core.Entities;
using BaseServiceApiRest_Core.Interfaces.IServices;
using BaseServiceApiRest_Core.Utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BaseServiceApiRest_Core.Services;

public class JwtProviderService : IJwtProviderService
{
    public JwtProviderService() { }

    public string GenerateToken(UserTypeEnum role, long userId)
    {
        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(Hashing.Key);

        var claims = new List<Claim>()
        {
            new Claim("id", userId.ToString()),
            new Claim(ClaimTypes.Role, role.ToString())
        };

        var descriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddHours(2),
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = new SigningCredentials(
                                 new SymmetricSecurityKey(key),
                                 SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handler.CreateToken(descriptor);

        return handler.WriteToken(token);
    }
}
