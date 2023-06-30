using Microsoft.Extensions.DependencyInjection;
using OfferApp.Core.Repositories;
using OfferApp.Infrastructure.Repositories;

namespace OfferApp.Infrastructure
{
    // Z racji że będziemy używać baz danych, plików wyodrębniłem repository do osobnego projektu.
    // Pozowoli to nam na możliwość podpięcia innej implementacji projektu a w tym przypadku Repository
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}