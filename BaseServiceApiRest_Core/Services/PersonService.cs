using AutoMapper;
using BaseServiceApiRest_Core.DTOs;
using BaseServiceApiRest_Core.Entities;
using BaseServiceApiRest_Core.Interfaces.IRepositories;
using BaseServiceApiRest_Core.Interfaces.IServices;
using BaseServiceApiRest_Core.Interfaces.Repositories;
using BaseServiceApiRest_Core.Interfaces.Repositories.Base;
using BaseServiceApiRest_Core.Services.Base;
using System.Net;
namespace BaseServiceApiRest_Core.Services;
public class PersonService : BaseService<PersonDTO, Person>, IPersonService
{
    public PersonService(IRepository<Person> repository, IMapper mapper) : base(repository, mapper)
    { }

    private readonly ISchoolRepository _schoolRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IUserRepository _userRepository;
    public PersonService(IRepository<Person> repository,
                         IMapper mapper,
                         ISchoolRepository schoolRepository,
                         IUserRepository userRepository,
                         IPersonRepository personRepository) : base(repository, mapper)
    {
        _schoolRepository = schoolRepository;
        _userRepository = userRepository;
        _personRepository = personRepository;
    }

    public override async Task<PersonDTO> Create(PersonDTO dto)
    {
        var user = await _userRepository.GetById(dto.UserId);

        if (user is null)
        {
            throw new HttpRequestException("User is not found");
        }

        var leader = user.UserType.HasFlag(UserTypeEnum.Leader);

        if (!leader)
        {
            throw new HttpRequestException("This user is not a Leader");
        }

        var school = await _schoolRepository.GetById(dto.SchoolId);

        if (school is null)
        {
            throw new HttpRequestException("School is not found");
        }

        Person newUser = new()
        {
            Name = dto.Name,
            IsTransfer = dto.IsTransfer,
            Age = dto.Age,
            Email = dto.Email,
            Phone = dto.Phone,
            Address = dto.Address,
            Neighborhood = dto.Neighborhood,
            City = dto.City,
            PostalCode = dto.PostalCode,
            Notes = dto.Notes,
            SchoolId = dto.SchoolId,
            UserId = dto.UserId,
        };

        newUser = await _repository.Create(newUser);

        return _mapper.Map<PersonDTO>(newUser);
    }

    public async Task<List<PersonNeighborhoodDTO>> GetPersonByNeighborhood()
    {
        return await _personRepository.GetPersonByNeighborhood();
    }

    public async Task<List<Person>> GetTransferVoters()
    {
        return await _personRepository.GetTransferVoters();
    }

    public override async Task<PersonDTO> Update(long? id, PersonDTO dto)
    {
        var newModel = _mapper.Map<Person>(dto);

        var originalModel = await _repository.GetById(id ?? newModel.Id, false);

        if (originalModel is null) throw new HttpRequestException($"Resource Id {id} not found", null, HttpStatusCode.NotFound);

        newModel.Id = originalModel.Id;

        originalModel.IsTransfer = newModel.IsTransfer;

        newModel = await _repository.Update(newModel);

        return _mapper.Map<PersonDTO>(newModel);
    }

    public override async Task<List<PersonDTO>> GetAll()
    {
        var result = await _repository.GetAll();

        var personDTOs = result.Select(person => new PersonDTO
        {
            Id = person.Id,
            Name = person.Name,
            IsTransfer = person.IsTransfer,
            Age = person.Age,
            Email = person.Email ?? "This user does not have Email",
            Phone = person.Phone,
            Address = person.Address,
            Neighborhood = person.Neighborhood,
            City = person.City,
            PostalCode = person.PostalCode,
            Notes = person.Notes,
            UserId = person.UserId,
            SchoolId = person.SchoolId,
            SchoolName = person.School?.Name ?? "Unknown School",
            LeaderName = person.UserId != null ? _userRepository.GetById(person.UserId)?.Result.Name ?? "Unknown Leader" : "No Leader Assigned"
        }).ToList();

        return personDTOs;
    }
}