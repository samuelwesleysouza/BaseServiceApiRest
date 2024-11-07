using BaseServiceApiRest_Core.DTOs;
using BaseServiceApiRest_Core.Entities;
using BaseServiceApiRest_Core.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseServiceApiRest_Core.Interfaces.Repositories;

public interface IUserRepository : IRepository<Users>
{
    Task<Users?> UserEmail(string email);
    Task<List<LeaderRegistersDTO>> GetLeaderAndYourPersonsRegister();
}