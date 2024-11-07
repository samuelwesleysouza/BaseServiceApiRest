using AutoMapper;
using BaseServiceApiRest_Core.DTOs;
using BaseServiceApiRest_Core.Entities;
using BaseServiceApiRest_Core.Interfaces.IServices;
using BaseServiceApiRest_Core.Interfaces.Repositories.Base;
using BaseServiceApiRest_Core.Services.Base;
namespace BaseServiceApiRest_Core.Services;

public class SchoolService : BaseService<SchoolDTO, School>, ISchoolService
{
    public SchoolService(IRepository<School> repository, IMapper mapper) : base(repository, mapper)
    {}
}