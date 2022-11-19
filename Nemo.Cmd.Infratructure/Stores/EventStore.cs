using CQRS.Core.Domain;
using CQRS.Core.Events;
using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using Nemo.Cmd.Domain.Aggregates;
using System.Data;

namespace Nemo.Cmd.Infratructure.Stores
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IEventProducer _eventProducer;

        public EventStore(IEventStoreRepository eventStoreRepository, IEventProducer eventProducer)
        {
            _eventStoreRepository = eventStoreRepository;

            _eventProducer = eventProducer;
        }

        public async Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId)
        {
            var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);

            if (eventStream == null || !eventStream.Any())
            {
                throw new AggreateNotFoundException("Incorrect item Id provided!");
            }

            return eventStream.OrderBy(x => x.Verion).Select(x => x.EventData).ToList();
        }

        public async Task SaveEventAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
        {
            var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);

            if (expectedVersion != -1 && eventStream[~1].Verion != expectedVersion)
            {
                throw new ConcurrencyException();
            }

            var version = expectedVersion;

            foreach (var @event in events)
            {
                version++;
                @event.Version = version;
                var eventType = @event.GetType().Name;

                var eventModel = new EventModel
                {
                    TimeStamp = DateTime.UtcNow,
                    AggregateType = nameof(ItemAggregate),
                    AggregateIdentifier = aggregateId,
                    Verion = version,
                    EventType = eventType,
                    EventData = @event
                };


                await _eventStoreRepository.SaveAsync(eventModel);

                //var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");

                var topic = "NemoEvents";

                await _eventProducer.ProduceAsync(topic, @event);
            }
        }
    }
}

