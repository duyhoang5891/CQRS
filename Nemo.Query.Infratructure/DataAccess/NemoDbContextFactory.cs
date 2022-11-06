using System;
using Microsoft.EntityFrameworkCore;

namespace Nemo.Query.Infratructure.DataAccess
{
    public class NemoDbContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public NemoDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        public NemoDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<NemoDbContext> optionsBuilder = new();

            _configureDbContext(optionsBuilder);

            return new NemoDbContext(optionsBuilder.Options);
        }



    }
}

