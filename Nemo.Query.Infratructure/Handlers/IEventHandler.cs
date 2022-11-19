using Nemo.Common.Events;

namespace Nemo.Query.Infratructure.Handlers
{
    public interface IEventHandler
    {
        Task On(CreatedItemEvent @event);
        Task On(UpdateItemEvent @event);
        Task On(DeleteItemEvent @event);
    }
}

