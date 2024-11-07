namespace BaseServiceApiRest_Core.DTOs;

public class PersonDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsTransfer { get; set; }
    public int Age { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public long SchoolId { get; set; }
    public long UserId { get; set; }

    public string? SchoolName { get; set; }
    public string? LeaderName { get; set; }
}