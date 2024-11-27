using Shared.Contracts.Enums;

namespace Users.Contracts.Dto;

public class ContactDto
{
    public required ContactType Type { get; set; }
    public required string Link { get; set; }
}