using Microsoft.Extensions.DependencyInjection;
using OfferApp.Core.DTO;
using OfferApp.Core.Repositories;
using OfferApp.Core.Services;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("OfferApp.UnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace OfferApp.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            return services
                    .AddScoped(_ => CreateMenuService())
                    .AddScoped<IBidService, BidService>();
        }

        private static IMenuService CreateMenuService()
        {
            return new MenuService(CreateMenus());
        }

        private static List<MenuDto> CreateMenus()
        {
            return new List<MenuDto>()
            {
                new MenuDto { Id = 1, Name = "Add offer" },
                new MenuDto { Id = 2, Name = "Show details" },
                new MenuDto { Id = 3, Name = "Update offer" },
                new MenuDto { Id = 4, Name = "Show all offers" },
                new MenuDto { Id = 5, Name = "Delete offer" },
                new MenuDto { Id = 6, Name = "Show all offers published" },
                new MenuDto { Id = 7, Name = "Publish or unpublish bid" },
                new MenuDto { Id = 8, Name = "Bid up the offer" },
                new MenuDto { Id = 9, Name = "Quit program" },
            };
        }
    }
}