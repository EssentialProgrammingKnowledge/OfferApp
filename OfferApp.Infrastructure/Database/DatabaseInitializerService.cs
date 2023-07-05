using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OfferApp.Infrastructure.Database
{
    internal sealed class DatabaseInitializerService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseInitializerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var scope = _serviceProvider.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.Start();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
