using CQRS.Core.Commands;

namespace Nemo.Cmd.Api.Commands
{
    public class AddItemCommand : BaseCommand
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
