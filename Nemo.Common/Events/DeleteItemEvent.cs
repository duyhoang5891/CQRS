using CQRS.Core.Events;

namespace Nemo.Common.Events
{
    public class DeleteItemEvent:BaseEvent
    {
        public DeleteItemEvent(): base(nameof(DeleteItemEvent))
        {

        }

        public Guid ItemId { get; set; }
    }
}
