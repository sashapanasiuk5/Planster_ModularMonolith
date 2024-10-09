namespace Shared.Api.Respones;

public class SuccessResponse: Response
{
    public object Data { get; set; }

    public SuccessResponse(object data)
    {
        Data = data;
        Success = true;
    }
}