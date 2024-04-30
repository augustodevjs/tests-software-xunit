using MediatR;
using NerdStore.Core.Messages;

namespace NerdStore.Core.DomainObjects;

public class DomainNotification : Message, INotification
{
    public string Key { get; private set; }
    public int Version { get; private set; }
    public string Value { get; private set; }
    public DateTime Timestamp { get; private set; }
    public Guid DomainNotificationId { get; private set; }

    public DomainNotification(string key, string value)
    {
        Key = key;
        Version = 1;
        Value = value;
        Timestamp = DateTime.Now;
        DomainNotificationId = Guid.NewGuid();
    }
}