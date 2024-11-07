using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseServiceApiRest_Core.Interfaces.IServices.Base;

public interface IService<DTO>
{
    Task<DTO> GetOne(long id);
    Task<List<DTO>> GetAll();
    Task<DTO> Create(DTO dto);
    Task<DTO> Update(long? id, DTO dto);
    Task Delete(long id);
}