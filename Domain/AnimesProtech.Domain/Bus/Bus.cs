using AnimesProtech.Domain.Interface.Bus;
using AnimesProtech.Domain.Interface.Notification;

namespace AnimesProtech.Domain.Bus;

public class Bus : IBus
{
    public INotify Notify { get; private set; }

    public Bus(INotify notify)
    {
        Notify = notify;
    }
}