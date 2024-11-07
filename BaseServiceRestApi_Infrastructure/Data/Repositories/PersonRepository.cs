using BaseServiceApiRest_Core.DTOs;
using BaseServiceApiRest_Core.Entities;
using BaseServiceApiRest_Core.Interfaces.IRepositories;
using BaseServiceApiRest_Core.Transactions;
using BaseServiceRestApi_Infrastructure.Data.Context;
using BaseServiceRestApi_Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BaseServiceRestApi_Infrastructure.Data.Repositories;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    public PersonRepository(BaseServiceRestApiContext context, ITransactionManager transactionManager) : base(context, transactionManager)
    { }

    public async Task<List<PersonNeighborhoodDTO>> GetPersonByNeighborhood()
    {
        return await _context.Set<Person>()
                             .GroupBy(x => x.Neighborhood)
                             .Select(x => new PersonNeighborhoodDTO
                             {
                                 Neighborhood = x.Key,
                                 QuantityPersons = x.Count()
                             }).ToListAsync();
    }

    public async Task<List<Person>> GetTransferVoters()
    {
        return await _context.Set<Person>()
                             .Where(x => x.IsTransfer)
                             .ToListAsync();
    }

    public override async Task<List<Person>> GetAll()
    {
        return await _context.Set<Person>()
                             .Include(x => x.School)
                             .Select(x => new Person
                             {
                                 Id = x.Id,
                                 Name = x.Name,
                                 UserId = x.UserId,
                                 Address = x.Address,
                                 Age = x.Age,
                                 City = x.City,
                                 CreatedAt = x.CreatedAt,
                                 DeletedAt = x.DeletedAt,
                                 Email = x.Email,
                                 IsDeleted = x.IsDeleted,
                                 IsTransfer = x.IsTransfer,
                                 Notes = x.Notes,
                                 Phone = x.Phone,
                                 PostalCode = x.PostalCode,
                                 SchoolId = x.SchoolId,
                                 School = x.School,
                             })
                             .ToListAsync();
    }
}