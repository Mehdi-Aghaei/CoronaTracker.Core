using System.Linq;
using System.Threading.Tasks;
using CoronaTracker.Core.Models.Countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CoronaTracker.Core.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Country> Countries { get; set; }

        public async ValueTask<Country> InsertCountryAsync(Country country)
        {
            using var broker =
                new StorageBroker(this.counfiguration);

            EntityEntry<Country> countryEntityEntry =
                await broker.Countries.AddAsync(country);

            await broker.SaveChangesAsync();

            return countryEntityEntry.Entity;
        }

        public IQueryable<Country> SelectAllCountries()
        {
            using var broker =
                new StorageBroker(this.counfiguration);

            return broker.Countries;
        }

        public async ValueTask<Country> UpdateCountryAsync(Country country)
        {
            using var broker =
                new StorageBroker(this.counfiguration);

            EntityEntry<Country> countryEntityEntry =
                broker.Countries.Update(country);

            await broker.SaveChangesAsync();

            return countryEntityEntry.Entity;
        }
    }
}
