using CQRS.Core.Events;

namespace Nemo.Common.Events
{
    public class CreatedItemEvent : BaseEvent
    {
        public CreatedItemEvent() : base(nameof(CreatedItemEvent))
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
    }
}
