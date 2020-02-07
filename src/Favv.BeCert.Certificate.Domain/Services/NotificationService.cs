using CQRSlite.Events;
using System.Threading.Tasks;

namespace Favv.BeCert.Certificate.Domain.Services
{

    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 05-2019
    /// Purpose     : Client Subscriber notification service
    /// </summary>
    public static class NotificationService
    {
        public static Task NotifyClientSubscribersAsync(IEvent @event)
        {
            //TODO: notification logic : push the event on a message queue through a service bus endpoint
            return Task.CompletedTask;
        }
    }
}
