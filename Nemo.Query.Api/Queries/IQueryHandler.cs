using Nemo.Query.Domain.Entities;

namespace Nemo.Query.Api.Queries
{
    public interface IQueryHandler
    {
        public Task<List<Item>> HandlerAsync(FindAllItemQuery query);
        public Task<Item?> HandlerAsync(FindItemByIdQuery query);
    }
}
