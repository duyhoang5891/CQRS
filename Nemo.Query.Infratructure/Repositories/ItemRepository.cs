using Microsoft.EntityFrameworkCore;
using Nemo.Query.Domain.Entities;
using Nemo.Query.Domain.Repositories;
using Nemo.Query.Infratructure.DataAccess;

namespace Nemo.Query.Infratructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly NemoDbContextFactory _contextFactory;

        public ItemRepository(NemoDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateAsync(Item item)
        {
            using NemoDbContext dbContext = _contextFactory.CreateDbContext();

            dbContext.Items.Add(item);

            _ = await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid itemId)
        {
            using NemoDbContext dbContext = _contextFactory.CreateDbContext();

            var item = await dbContext.Items.Where(x => x.ItemId == itemId).FirstOrDefaultAsync();

            if (item != null)
            {
                dbContext.Items.Remove(item);

                _ = await dbContext.SaveChangesAsync();
            }
        }

        public async Task<Item?> GetByIdAsync(Guid itemId)
        {
            using NemoDbContext dbContext = _contextFactory.CreateDbContext();

            return await dbContext.Items.Where(x => x.ItemId == itemId).FirstOrDefaultAsync();
        }

        public async Task<List<Item>> ListAllAsync()
        {
            using NemoDbContext dbContext = _contextFactory.CreateDbContext();

            return await dbContext.Items.ToListAsync();
        }

        public async Task<List<Item>> ListByNameAsync(string name)
        {
            using NemoDbContext dbContext = _contextFactory.CreateDbContext();

            return await dbContext.Items.Where(x => x.Name == name).ToListAsync();
        }

        public async Task UpdateAsync(Item item)
        {
            using NemoDbContext dbContext = _contextFactory.CreateDbContext();

            var result = await dbContext.Items.Where(x => x.ItemId == item.ItemId).FirstOrDefaultAsync();

            if (result != null)
            {
                result.Name = item.Name;
                result.Description = item.Description;

                _ = await dbContext.SaveChangesAsync();
            }
        }
    }
}

