using BaseServiceApiRest_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BaseServiceApiRest_Core.Interfaces.IServices;

public interface IJwtProviderService
{
    string GenerateToken(UserTypeEnum role, long userId);
}
