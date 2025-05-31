namespace Identity.Contracts.Dtos;

public class LoginResultDto
{
    public string UserToken { get; set; }
    public string? ProjectToken { get; set; }
    public string RefreshToken { get; set; }

    public LoginResultDto(string userToken, string? projectToken, string refreshToken)
    {
        UserToken = userToken;
        ProjectToken = projectToken;
        RefreshToken = refreshToken;
    }
}