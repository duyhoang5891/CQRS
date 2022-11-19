namespace Nemo.Cmd.Api.Commands
{
    public interface ICommandHandler
    {
        public Task HandlerAsync(AddItemCommand command);
        public Task HandlerAsync(EditItemCommand command);
        public Task HandlerAsync(DeleteItemCommand command);
    }
}

