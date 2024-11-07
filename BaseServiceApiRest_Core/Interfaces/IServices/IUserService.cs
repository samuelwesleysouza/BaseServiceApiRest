using BaseServiceApiRest_Core.DTOs;
using BaseServiceApiRest_Core.Interfaces.IServices.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BaseServiceApiRest_Core.Interfaces.IServices;

public interface IUserService : IService<UserDTO>
{
    Task<List<LeaderRegistersDTO>> CountVotersByLeaders();
}
