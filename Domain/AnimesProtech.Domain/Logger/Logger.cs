namespace AnimesProtech.Domain.Logger;

public class Logger
{
    public string Metodo { get; private set; }
    public string? Body { get; private set; }
    public DateTimeOffset Data { get; private set; }

    public Logger(string metodo, string? body)
    {
        Metodo = metodo;
        Body = body;
        Data = DateTimeOffset.UtcNow;
    }
}