using BaseServiceApiRest_Core.Entities;
using BaseServiceApiRest_Core.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseServiceApiRest_Core.Interfaces.IRepositories;

public interface IManagerRepository : IRepository<Manager>
{
    Task<List<Users>> GetLeaders();
    Task<Manager?> GetManagerById(long? id);
}
