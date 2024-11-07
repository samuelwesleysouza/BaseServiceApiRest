using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseServiceApiRest_Core.Interfaces.Repositories.Base;

public interface IUserSensitive
{
    public long UserId { get; set; }
}
