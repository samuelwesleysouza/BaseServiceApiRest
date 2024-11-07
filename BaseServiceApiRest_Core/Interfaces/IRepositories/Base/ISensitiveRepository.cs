using BaseServiceApiRest_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseServiceApiRest_Core.Interfaces.Repositories.Base;
public interface ISensitiveRepository<M> : IRepository<M>
    where M : BaseEntity, IUserSensitive
{ }