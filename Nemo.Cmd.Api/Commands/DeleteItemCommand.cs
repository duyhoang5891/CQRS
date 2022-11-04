using CQRS.Core.Commands;

namespace Nemo.Cmd.Api.Commands
{
    public class DeleteItemCommand: BaseCommand
    {
        public Guid ItemId { get; set; }
    }
}
