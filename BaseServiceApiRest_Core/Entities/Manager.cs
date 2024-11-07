namespace BaseServiceApiRest_Core.Entities;

public class Manager : BaseEntity
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
    public UserTypeEnum Role { get => UserTypeEnum.Manager; }

    public List<Users>? Leaders { get; set; }
}