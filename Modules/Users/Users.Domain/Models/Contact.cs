using Shared.Contracts.Enums;

namespace Users.Domain.Models;

public class Contact
{
    public int Id { get; set; }
    public string Value { get; set; }
    public ContactType Type { get; set; }  
}