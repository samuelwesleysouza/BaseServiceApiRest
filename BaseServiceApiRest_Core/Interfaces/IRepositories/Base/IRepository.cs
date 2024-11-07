using BaseServiceApiRest_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseServiceApiRest_Core.Interfaces.Repositories.Base;

public interface IRepository<M> where M : BaseEntity
{
    Task<M?> GetById(long id, bool entityTracking = true);
    Task<M?> SearchOne(Expression<Func<M, bool>> expression);
    Task<List<M>> GetAll();
    Task<M?> Create(M model);
    Task<M?> Update(M model);
    Task SoftDelete(M model);
    Task HardDelete(M model);
}