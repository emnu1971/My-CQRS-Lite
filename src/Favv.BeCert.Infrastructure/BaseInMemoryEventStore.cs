using CQRSlite.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Favv.BeCert.Infrastructure
{

    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 05-2019
    /// Purpose     : In memory event store base class
    /// </summary>
    public abstract class BaseInMemoryEventStore : IEventStore
    {
        private readonly IEventPublisher _publisher;
        private readonly Dictionary<Guid, List<IEvent>> _inMemoryDb = new Dictionary<Guid, List<IEvent>>();

        public BaseInMemoryEventStore(IEventPublisher publisher)
        {
            _publisher = publisher;
        }

        public virtual async Task SaveAsync(IEnumerable<IEvent> events, CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var @event in events)
            {
                _inMemoryDb.TryGetValue(@event.Id, out var list);
                if (list == null)
                {
                    list = new List<IEvent>();
                    _inMemoryDb.Add(@event.Id, list);
                }
                list.Add(@event);
                await _publisher.PublishAsync(@event, cancellationToken);
            }
        }

        public virtual Task<IEnumerable<IEvent>> Get(Guid aggregateId, int fromVersion, CancellationToken cancellationToken = default(CancellationToken))
        {
            _inMemoryDb.TryGetValue(aggregateId, out var events);
            return Task.FromResult(events?.Where(x => x.Version > fromVersion) ?? new List<IEvent>());
        }
    }
}
