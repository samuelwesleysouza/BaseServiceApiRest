using BaseServiceApiRest_Core.Entities;
using BaseServiceApiRest_Core.Interfaces.IRepositories;
using BaseServiceApiRest_Core.Security;
using BaseServiceApiRest_Core.Transactions;
using BaseServiceRestApi_Infrastructure.Data.Context;
using BaseServiceRestApi_Infrastructure.Data.Repositories.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BaseServiceRestApi_Infrastructure.Data.Repositories;

public class ManagerRepository : BaseRepository<Manager>, IManagerRepository
{
    private readonly UserManager _userManager;

    public ManagerRepository(BaseServiceRestApiContext context, ITransactionManager transactionManager, IHttpContextAccessor httpContextAccessor) : base(context, transactionManager)
    {
        _userManager = new UserManager(httpContextAccessor);
    }

    public async Task<Manager?> GetManagerById(long? id)
    {
        return await _context.Set<Manager>()
                             .Where(m => m.Id == id)
                             .FirstOrDefaultAsync();
    }


    public async Task<List<Users>> GetLeaders()
    {
        if (_userManager.IsManager)
        {
            return await _context.Set<Users>()
               .Where(x => x.UserType == UserTypeEnum.Leader && x.ManagerId == _userManager.currentUser)
               .ToListAsync();
        }

        if (_userManager.IsAdmin)
        {
            return await _context.Set<Users>()
                           .Where(x => x.UserType == UserTypeEnum.Leader)
                           .ToListAsync();
        }

        throw new HttpRequestException("This service is only Managers users", null, System.Net.HttpStatusCode.Forbidden);
    }
}