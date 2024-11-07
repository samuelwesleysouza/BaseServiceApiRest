using BaseServiceApiRest_Core.DTOs;
using BaseServiceApiRest_Core.Entities;
using BaseServiceApiRest_Core.Interfaces.IServices.Base;
namespace BaseServiceApiRest_Core.Interfaces.IServices;

public interface IPersonService : IService<PersonDTO>
{
    Task<List<PersonNeighborhoodDTO>> GetPersonByNeighborhood();
    Task<List<Person>> GetTransferVoters(); 
}