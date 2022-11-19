using Nemo.Common.Events;
using Nemo.Query.Domain.Entities;
using Nemo.Query.Domain.Repositories;

namespace Nemo.Query.Infratructure.Handlers
{
    public class EventHandler : IEventHandler
    {
        private readonly IItemRepository _itemRepository;

        public EventHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task On(CreatedItemEvent @event)
        {
            var item = new Item
            {
                ItemId = @event.Id,
                Name = @event.Name,
                Description = @event.Description,
                Created = @event.Created
            };

            await _itemRepository.CreateAsync(item);
        }

        public async Task On(UpdateItemEvent @event)
        {
            var item = await _itemRepository.GetByIdAsync(@event.Id);

            if (item == null) return;

            item.Description = @event.Description;

            await _itemRepository.UpdateAsync(item);
        }

        public async Task On(DeleteItemEvent @event)
        {
            var item = await _itemRepository.GetByIdAsync(@event.Id);

            if (item == null) return;

            await _itemRepository.DeleteAsync(item.ItemId);
        }
    }
}

