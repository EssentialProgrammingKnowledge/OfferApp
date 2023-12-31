﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OfferApp.Infrastructure.Repositories;
using OfferApp.Migrations;

namespace OfferApp.Infrastructure.Database
{
    internal static class Extensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddEFCore();
            return services;
        }

        internal static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            var databaseOptions = services.GetService<IOptions<DatabaseOptions>>();
            var serverVersion = new MySqlServerVersion(databaseOptions.Value.Version);
            services.AddDbContext<OfferDbContext>(options => options.UseMySql(databaseOptions.Value.ConnectionString, serverVersion));
            return services;
        }

        internal static IServiceCollection AddInMemoryStoreProvider(this IServiceCollection services)
        {
            services.AddInMemoryRepositories();
            return services;
        }

        internal static IServiceCollection AddFileStoreProvider(this IServiceCollection services)
        {
            services.AddFileRepositories();
            return services;
        }

        internal static IServiceCollection AddDapper(this IServiceCollection services)
        {
            services.AddDapperRepositories();
            services.AddTransient<IDbInitializer, DapperDbInitializer>();
            var databaseOptions = services.GetService<IOptions<DatabaseOptions>>();
            services.AddMigrations(databaseOptions.Value.ConnectionString!);
            services.AddHostedService<DatabaseInitializerService>();
            return services;
        }

        internal static IServiceCollection AddEFCore(this IServiceCollection services)
        {
            services.AddDbContext();
            services.AddEFCoreRepositories();
            services.AddTransient<IDbInitializer, EFDbInitializer>();
            services.AddHostedService<DatabaseInitializerService>();
            return services;
        }
    }
}
