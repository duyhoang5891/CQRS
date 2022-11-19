using Microsoft.EntityFrameworkCore;
using Nemo.Query.Domain.Entities;

namespace Nemo.Query.Infratructure.DataAccess
{
    public class NemoDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public NemoDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Nemo");
        }
    }
}

