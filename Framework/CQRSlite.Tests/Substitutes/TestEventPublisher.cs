using System.Threading;
using System.Threading.Tasks;
using CQRSlite.Events;

namespace CQRSlite.Tests.Substitutes
{
    public class TestEventPublisher : IEventPublisher
    {
        public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default(CancellationToken)) where T : class, IEvent
        {
            Published++;
            Token = cancellationToken;
            return Task.CompletedTask;
        }

        public CancellationToken Token { get; set; }
        public int Published { get; private set; }
    }
}