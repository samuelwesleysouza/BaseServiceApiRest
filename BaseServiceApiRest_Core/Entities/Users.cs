namespace BaseServiceApiRest_Core.Entities;

public class Users : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public string? Photo { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public long? ManagerId { get; set; }

    public UserTypeEnum UserType { get; set; }
    public Manager Manager { get; set; }
    public List<Person> QuantityPersons { get; set; }
}