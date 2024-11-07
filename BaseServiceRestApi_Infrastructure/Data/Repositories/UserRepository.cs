using BaseServiceApiRest_Core.DTOs;
using BaseServiceApiRest_Core.Entities;
using BaseServiceApiRest_Core.Interfaces.Repositories;
using BaseServiceApiRest_Core.Transactions;
using BaseServiceRestApi_Infrastructure.Data.Context;
using BaseServiceRestApi_Infrastructure.Data.Repositories.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
namespace BaseServiceRestApi_Infrastructure.Data.Repositories;

public class UserRepository : BaseRepository<Users>, IUserRepository
{

    public UserRepository(BaseServiceRestApiContext context, ITransactionManager transactionManager, IHttpContextAccessor httpContextAccessor) : base(context, transactionManager)
    {
        _context = context;
        _transaction = transactionManager;
    }

    public async Task<Users?> UserEmail(string email)
    {
        return await _context.Set<Users>()
                             .Where(x => x.Email == email)
                             .FirstOrDefaultAsync();
    }

    public async Task<List<LeaderRegistersDTO>> GetLeaderAndYourPersonsRegister()
    {
        return await _context.Set<Users>()
                             .Where(u => u.UserType == UserTypeEnum.Leader)
                             .Select(u => new LeaderRegistersDTO
                             {
                                 Id = u.Id,
                                 LeaderName = u.Name,
                                 Address = u.Address,
                                 City = u.City,
                                 Email = u.Email,
                                 Number = u.Number,
                                 Password = u.Password,
                                 Phone = u.Phone,
                                 Photo = u.Photo,
                                 PostalCode = u.PostalCode,
                                 QuantityPersons = u.QuantityPersons.Count(),
                             })
                             .ToListAsync();
    }
}