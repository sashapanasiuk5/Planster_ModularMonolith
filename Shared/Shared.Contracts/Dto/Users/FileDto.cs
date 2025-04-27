namespace Users.Contracts.Dto;

public class FileDto
{
    public byte[] Data { get; set; }
    public string FileName { get; set; }
    public string MimeType { get; set; }
}