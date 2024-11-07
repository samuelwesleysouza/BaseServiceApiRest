using BaseServiceApiRest_Core.Entities;
using BaseServiceApiRest_Core.Interfaces.IRepositories;
using BaseServiceApiRest_Core.Transactions;
using BaseServiceRestApi_Infrastructure.Data.Context;
using BaseServiceRestApi_Infrastructure.Data.Repositories.Base;


namespace BaseServiceRestApi_Infrastructure.Data.Repositories;

public class SchoolRepository : BaseRepository<School>, ISchoolRepository
{
    public SchoolRepository(BaseServiceRestApiContext context, ITransactionManager transactionManager) : base(context, transactionManager)
    {
    }
}