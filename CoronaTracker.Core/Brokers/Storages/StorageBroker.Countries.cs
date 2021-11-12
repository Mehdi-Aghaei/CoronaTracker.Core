using CoronaTracker.Core.Models.Countries;
using Microsoft.EntityFrameworkCore;

namespace CoronaTracker.Core.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Country> Countries { get; set; }
    }
}
