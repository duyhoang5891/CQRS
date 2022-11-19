using Nemo.Query.Domain.Entities;

namespace Nemo.Query.Domain.Repositories
{
    public interface IItemRepository
    {
        Task CreateAsync(Item item);
        Task UpdateAsync(Item item);
        Task DeleteAsync(Guid itemId);
        Task<Item?> GetByIdAsync(Guid itemId);
        Task<List<Item>> ListAllAsync();
        Task<List<Item>> ListByNameAsync(string name);
    }
}