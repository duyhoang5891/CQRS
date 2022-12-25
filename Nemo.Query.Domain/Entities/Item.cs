using CQRS.Core.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo.Query.Domain.Entities
{
    [Table(nameof(Item))]
    public class Item : BaseEntity
    {
        [Key]
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
    }
}

