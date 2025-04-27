using Users.Domain.Enums;

namespace Users.Domain.Models;

public class ProfilePhoto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public PhotoResolution Resolution { get; set; }
    public string FileName { get; set; }
    public string MimeType { get; set; }
    public byte[] Data { get; set; }
}