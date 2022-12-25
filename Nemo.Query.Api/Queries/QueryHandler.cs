using Nemo.Query.Domain.Entities;
using Nemo.Query.Domain.Repositories;

namespace Nemo.Query.Api.Queries
{
    public class QueryHandler : IQueryHandler
    {
        private readonly IItemRepository _itemRepository;

        public QueryHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<List<Item>> HandlerAsync(FindAllItemQuery query)
            => await _itemRepository.ListAllAsync();

        public async Task<Item?> HandlerAsync(FindItemByIdQuery query)
            => await _itemRepository.GetByIdAsync(query.Id);
    }
}
