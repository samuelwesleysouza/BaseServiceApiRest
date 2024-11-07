using BaseServiceApiRest_Core.DTOs;
using BaseServiceApiRest_Core.Entities;
using BaseServiceApiRest_Core.Interfaces.IServices.Base;
namespace BaseServiceApiRest_Core.Interfaces.IServices;

public interface IManagerService : IService<ManagerDTO>
{
    Task<List<Users>> GetLeaders();
}