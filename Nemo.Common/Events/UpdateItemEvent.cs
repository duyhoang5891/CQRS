using CQRS.Core.Events;

namespace Nemo.Common.Events
{
    public class UpdateItemEvent : BaseEvent
    {
        public UpdateItemEvent() : base(nameof(UpdateItemEvent))
        {

        }

        public string Description { get; set; }
    }
}
