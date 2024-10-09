using System.Text.Json.Serialization;

namespace AnimesProtech.Domain.Notification;

public class Notification
{
    [JsonPropertyOrder(-2)]
    public string Code { get; }
    
    [JsonPropertyOrder(-1)]
    public string Message { get; }

    public Notification(string code, string message)
    {
        Code = code;
        Message = message;
    }
}