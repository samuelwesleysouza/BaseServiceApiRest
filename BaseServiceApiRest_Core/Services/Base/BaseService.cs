using System.Net;
using AutoMapper;
using BaseServiceApiRest_Core.Entities;
using BaseServiceApiRest_Core.Interfaces.IServices.Base;
using BaseServiceApiRest_Core.Interfaces.Repositories.Base;
namespace BaseServiceApiRest_Core.Services.Base;

public abstract class BaseService<DTO, M> : IService<DTO>
    where M : BaseEntity
{
    protected IRepository<M> _repository;
    protected IMapper _mapper;

    public BaseService(IRepository<M> repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }
    public virtual async Task<DTO> Create(DTO dto)
    {
        var model = _mapper.Map<M>(dto);

        model = await _repository.Create(model);

        return _mapper.Map<DTO>(model);
    }
    public virtual async Task<List<DTO>> GetAll()
    {
        var result = await _repository.GetAll();

        return _mapper.Map<List<DTO>>(result);
    }
    public virtual async Task<DTO> GetOne(long id)
    {
        var result = await _repository.GetById(id);

        if (result is null) throw new HttpRequestException($"Resource Id {id} not found", null, HttpStatusCode.NotFound);

        return _mapper.Map<DTO>(result);
    }
    public virtual async Task<DTO> Update(long? id, DTO dto)
    {
        var newModel = _mapper.Map<M>(dto);

        var originalModel = await _repository.GetById(id ?? newModel.Id, false);

        if (originalModel is null) throw new HttpRequestException($"Resource Id {id} not found", null, HttpStatusCode.NotFound);

        newModel.Id = originalModel.Id;

        newModel = await _repository.Update(newModel);

        return _mapper.Map<DTO>(newModel);
    }
    public virtual async Task Delete(long id)
    {
        var model = await _repository.GetById(id);

        if (model is null) throw new HttpRequestException($"Resource Id {id} not found", null, HttpStatusCode.NotFound);

        await _repository.HardDelete(model);
    }
}