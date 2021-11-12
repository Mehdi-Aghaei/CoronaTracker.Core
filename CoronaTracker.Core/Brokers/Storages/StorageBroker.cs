using EFxceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CoronaTracker.Core.Brokers.Storages
{
    public partial class StorageBroker : EFxceptionsContext, IStorageBroker
    {
        private readonly IConfiguration counfiguration;
        public StorageBroker(IConfiguration configuration)
        {
            this.counfiguration = configuration;
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = this.counfiguration
                .GetConnectionString(name: "DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }

        public override void Dispose()
        { }
    }
}
