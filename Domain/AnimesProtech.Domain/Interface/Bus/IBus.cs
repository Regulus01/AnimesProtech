using AnimesProtech.Domain.Interface.Notification;

namespace AnimesProtech.Domain.Interface.Bus;

public interface IBus
{
    INotify Notify { get; }
}