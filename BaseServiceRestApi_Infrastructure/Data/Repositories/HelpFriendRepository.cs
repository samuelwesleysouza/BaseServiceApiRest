
using BaseServiceApiRest_Core.Entities;
using BaseServiceApiRest_Core.Interfaces.IRepositories;
using BaseServiceApiRest_Core.Transactions;
using BaseServiceRestApi_Infrastructure.Data.Context;
using BaseServiceRestApi_Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;



namespace BaseServiceRestApi_Infrastructure.Data.Repositories;

public class HelpFriendRepository : BaseRepository<HelpFriend>, IHelpFriendRepository
{
    public HelpFriendRepository(BaseServiceRestApiContext context, ITransactionManager transactionManager) : base(context, transactionManager)
    { }

    public override async Task<List<HelpFriend>> GetAll()
    {
        return await _context.Set<HelpFriend>()
                             .Include(p => p.Person)
                             .Include(u => u.Users)
                             .Select(x => new HelpFriend
                             {
                                 Id = x.Id,
                                 PersonId = x.PersonId,
                                 UserId = x.UserId,
                                 WhyHelp = x.WhyHelp,
                                 Person = new Person
                                 {
                                     Name = x.Person.Name,
                                 },
                                 Users = new Users
                                 {
                                     Name = x.Users.Name,
                                 }
                             })
                             .ToListAsync();
    }
}