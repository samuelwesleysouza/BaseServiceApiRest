namespace BaseServiceApiRest_Core.DTOs;

public class LeaderRegistersDTO
{
    public long Id { get; set; }
    public string LeaderName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public int QuantityPersons { get; set; }
}