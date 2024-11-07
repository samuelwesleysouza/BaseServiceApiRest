namespace BaseServiceApiRest_Core.DTOs;

public class HelpFriendDTO
{
    public long Id { get; set; }
    public long PersonId { get; set; }
    public string? PersonName { get; set; } = string.Empty;
    public long UserId { get; set; }
    public string? LeaderName { get; set; } = string.Empty;
    public string WhyHelp { get; set; } = string.Empty;
}