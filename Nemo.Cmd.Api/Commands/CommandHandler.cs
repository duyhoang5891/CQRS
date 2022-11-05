using System;
using CQRS.Core.Handlers;
using Nemo.Cmd.Domain.Aggregates;

namespace Nemo.Cmd.Api.Commands
{
    public class CommandHandler : ICommandHandler
    {
        private readonly IEventSourcingHandler<ItemAggregate> _eventSourcingHandler;

        public CommandHandler(IEventSourcingHandler<ItemAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task HandlerAsync(AddItemCommand command)
        {
            var aggregate = new ItemAggregate(command.Id, command.Name, command.Description);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandlerAsync(EditItemCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);

            aggregate.EditItem(command.Description);
        }

        public async Task HandlerAsync(DeleteItemCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);

            aggregate.DeleteItem(command);
        }
    }
}

