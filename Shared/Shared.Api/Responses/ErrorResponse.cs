namespace Shared.Api.Respones;

public class ErrorResponse: Response
{
    public List<string> Errors { get; set; }

    public ErrorResponse(List<string> errors)
    {
        Success = false;
        Errors = errors;
    }
}