using System;
using CQRS.Core.Domain;
using CQRS.Core.Events;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Nemo.Cmd.Infratructure.Config;

namespace Nemo.Cmd.Infratructure.Repositories
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly IMongoCollection<EventModel> _eventStoreCollection;

        public EventStoreRepository(IOptions<MongoDbConfig> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.Value.DataBase);

            _eventStoreCollection = mongoDatabase.GetCollection<EventModel>(options.Value.Collection);
        }

        public async Task<List<EventModel>> FindByAggregateId(Guid aggreagateId)
            => await _eventStoreCollection.Find(x => x.AggregateIdentifier == aggreagateId).ToListAsync().ConfigureAwait(false);

        public async Task SaveAsync(EventModel @event)
            => await _eventStoreCollection.InsertOneAsync(@event).ConfigureAwait(false);
    }
}

