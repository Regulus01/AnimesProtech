namespace AnimesProtech.Infra.CrossCutting.Notification.Response;

public class SuccessResponse
{
    public bool Success { get; private set; }
    public DateTimeOffset Time { get; private set; }
    public object? Data { get; private set; }

    public SuccessResponse(object? data = null)
    {
        Success = true;
        Time = DateTimeOffset.UtcNow;
        Data = data;
    }
}