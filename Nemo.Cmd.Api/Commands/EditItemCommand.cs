using CQRS.Core.Commands;

namespace Nemo.Cmd.Api.Commands
{
    public class EditItemCommand : BaseCommand
    {
        public Guid ItemId { get; set; }
        public string Description { get; set; }
    }
}
