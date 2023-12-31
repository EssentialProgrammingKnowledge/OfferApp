﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using OfferApp.Domain.Repositories;
using OfferApp.Infrastructure.Cache;
using OfferApp.Infrastructure.Database;
using System.Data;

namespace OfferApp.Infrastructure.Repositories
{
    internal static class Extensions
    {
        public static IServiceCollection AddInMemoryRepositories(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<IBidRepository, InMemoryBidRepository>();
            return services;
        }

        public static IServiceCollection AddFileRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(XmlRepository<>));
            services.AddSingleton<XmlBidRepository>();
            services.AddScoped(typeof(XmlRepository<>));
            services.AddCache();
            services.AddScoped<IBidRepository>(sp =>
            {
                var bidRepository = sp.GetRequiredService<XmlBidRepository>();
                var cacheWrapper = sp.GetRequiredService<ICacheWrapper>();
                return new CacheBidRepository(bidRepository, cacheWrapper);
            });
            return services;
        }

        public static IServiceCollection AddDapperRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDbConnection, MySqlConnection>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<DatabaseOptions>>();
                return new MySqlConnection(options.Value.ConnectionString!);
            });
            services.AddCache();
            services.AddScoped<DapperBidRepository>();
            services.AddScoped<IBidRepository>(sp =>
            {
                var bidRepository = sp.GetRequiredService<DapperBidRepository>();
                var cacheWrapper = sp.GetRequiredService<ICacheWrapper>();
                return new CacheBidRepository(bidRepository, cacheWrapper);
            });
            return services;
        }

        public static IServiceCollection AddEFCoreRepositories(this IServiceCollection services)
        {
            services.AddCache();
            services.AddScoped<EFBidRepository>();
            services.AddScoped<IBidRepository>(sp =>
            {
                var bidRepository = sp.GetRequiredService<EFBidRepository>();
                var cacheWrapper = sp.GetRequiredService<ICacheWrapper>();
                return new CacheBidRepository(bidRepository, cacheWrapper);
            });
            return services;
        }
    }
}
