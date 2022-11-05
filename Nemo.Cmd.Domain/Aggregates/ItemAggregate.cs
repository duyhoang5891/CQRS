using System;
using CQRS.Core.Domain;
using Nemo.Common.Events;

namespace Nemo.Cmd.Domain.Aggregates
{
    public class ItemAggregate: AggregateRoot
    {
        private bool _isActive;
        private string _name;

        public bool Active
        {
            get => _isActive; set => _isActive = value;
        }

        public ItemAggregate()
        {

        }

        public ItemAggregate(Guid id, string name, string description)
        {
            RaiseEvent(new CreatedItemEvent
            {
                Id = id,
                Name = name,
                Description = description,
                Created = DateTime.UtcNow
            });


        }

        public void Apply(CreatedItemEvent @event)
        {
            _id = @event.Id;
            _isActive = true;
            _name = @event.Name;
        }

        public void EditItem(string decription)
        {
            if (!_isActive)
            {
                throw new InvalidOperationException("You can't edit the description of inactive item!");
            }

            if (string.IsNullOrEmpty(decription))
            {
                throw new InvalidOperationException($"The value of {nameof(decription)} can't be null or empty!");
            }

            RaiseEvent(new UpdateItemEvent
            {
                Id = _id,
                Description = decription
            });
        }

        public void Apply(UpdateItemEvent @event)
        {
            _id = @event.Id;
        }

        public void DeleteItem(DeleteItemEvent @event)
        {
            if (!_isActive)
            {
                throw new InvalidOperationException("You can't delete item inactive!");
            }

            RaiseEvent(new DeleteItemEvent
            {
                Id = @event.Id
            });
        }

        public void Apply(DeleteItemEvent @event)
        {
            _id = @event.Id;
        }
    }
}

