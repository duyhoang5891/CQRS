using System;
using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using Nemo.Cmd.Domain.Aggregates;

namespace Nemo.Cmd.Infratructure.Handlers
{
    public class EventSourcingHandler : IEventSourcingHandler<ItemAggregate>
    {
        private readonly IEventStore _eventStore;

        public EventSourcingHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<ItemAggregate> GetByIdAsync(Guid aggregateId)
        {
            var aggragate = new ItemAggregate();

            var events = await _eventStore.GetEventsAsync(aggregateId);

            if(events == null || !events.Any())
            {
                return aggragate;
            }

            aggragate.ReplayEvents(events);

            aggragate.Verion = events.Max(x => x.Version);

            return aggragate;
        }

        public async Task SaveAsync(AggregateRoot aggregate)
        {
            await _eventStore.SaveEventAsync(aggregate.Id, aggregate.GetUnCommittedChanges(), aggregate.Verion);

            aggregate.MarkChangesAsCommitted();
        }
    }
}

