using CQRS.Core.Infrastructure;
using CQRS.Core.Queries;
using Nemo.Query.Domain.Entities;

namespace Nemo.Query.Infratructure.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher<Item>
    {
        private readonly Dictionary<Type, Func<BaseQuery, Task<List<Item>>>> _handlers = new();

        public void RegisterHandler<TQuery>(Func<TQuery, Task<List<Item>>> handler) where TQuery : BaseQuery
        {
            if (_handlers.ContainsKey(typeof(TQuery)))
            {
                throw new IndexOutOfRangeException("You can't create the same query handler!");
            }

            _handlers.Add(typeof(TQuery), x => handler((TQuery)x));
        }

        public async Task<List<Item>> SendAsync(BaseQuery query)
        {
            if(_handlers.TryGetValue(query.GetType(), out Func<BaseQuery, Task<List<Item>>>? handler))
            {
                return await handler(query);
            }

            throw new ArgumentNullException(nameof(handler), "No query hanlder was registered!");
        }
    }
}
