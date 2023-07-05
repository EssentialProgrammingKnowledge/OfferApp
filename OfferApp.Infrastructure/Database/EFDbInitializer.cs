using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace OfferApp.Infrastructure.Database
{
    internal class EFDbInitializer : IDbInitializer
    {
        private readonly OfferDbContext _dbContext;
        private readonly DatabaseOptions _databaseOptions;

        public EFDbInitializer(OfferDbContext dbContext, IOptions<DatabaseOptions> databaseOptions)
        {
            _dbContext = dbContext;
            _databaseOptions = databaseOptions.Value;
        }

        public async Task Start()
        {
            if (!_databaseOptions.RunMigrationsOnStart)
            {
                return;
            }

            await _dbContext.Database.MigrateAsync();
        }
    }
}
