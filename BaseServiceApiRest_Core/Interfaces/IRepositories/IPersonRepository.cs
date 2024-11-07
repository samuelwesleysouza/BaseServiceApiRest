using BaseServiceApiRest_Core.DTOs;
using BaseServiceApiRest_Core.Entities;
using BaseServiceApiRest_Core.Interfaces.Repositories.Base;

namespace BaseServiceApiRest_Core.Interfaces.IRepositories;
public interface IPersonRepository : IRepository<Person>
{
    Task<List<PersonNeighborhoodDTO>> GetPersonByNeighborhood();
    Task<List<Person>> GetTransferVoters();
}