namespace Domain.Models;

public class RefreshToken
{
    public int IdentityId { get; set; }
    public Identity Identity { get; set; }
    public string AccessToken { get; set; }
    public string? ProjectToken { get; set; }
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }
    
    public bool IsExpired => ExpiryDate < DateTime.UtcNow;

    public RefreshToken(int identityId, string accessToken, string? projectToken, string token, DateTime expiryDate)
    {
        IdentityId = identityId;
        AccessToken = accessToken;
        ProjectToken = projectToken;
        Token = token;
        ExpiryDate = expiryDate;
    }
}