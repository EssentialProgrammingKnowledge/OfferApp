using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OfferApp.Infrastructure.Cache;
using OfferApp.Infrastructure.Database;
using OfferApp.Infrastructure.Exceptions;
using System.Text.Json;

namespace OfferApp.Infrastructure
{
    public static class Extensions
    {
        #region ConsoleApp

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterOptions(configuration);
            services.AddDatabase();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddErrorHandling();
            return services;
        }

        private static IServiceCollection RegisterOptions(this IServiceCollection services, IConfiguration configuration)
        {
            return services.Configure<DatabaseOptions>(configuration.GetSection("database"))
                           .Configure<AppOptions>(configuration.GetSection("app"))
                           .Configure<CacheOptions>(configuration.GetSection("cache"));
        }

        public static WebApplication UseInfrastructure(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseErrorHandling();
            app.MapControllers();
            return app;
        }

        #endregion

        #region ConsoleApp

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, Dictionary<string, object> appsettings)
        {
            return services.RegisterAppSettings(appsettings)
                           .AddDatabase();
        }

        private static IServiceCollection RegisterAppSettings(this IServiceCollection services, Dictionary<string, object> appsettings)
        {
            return services.RegisterCacheOptions(appsettings)
                           .RegisterDatabaseOptions(appsettings);
        }

        public static IServiceProvider UseInfrastructure(this IServiceProvider serviceProvider)
        {
            var dbInitializer = serviceProvider.GetRequiredService<IDbInitializer>();
            dbInitializer.Start().GetAwaiter().GetResult();
            return serviceProvider;
        }

        private static IServiceCollection RegisterCacheOptions(this IServiceCollection services, Dictionary<string, object> appsettings)
        {
            var cacheOptionsJsonElement = (JsonElement)(appsettings["cache"]
                ?? throw new InvalidOperationException("Cannot find section 'cache', please ensure that this file exists"));
            var cacheOptions = cacheOptionsJsonElement.Deserialize<CacheOptions>(new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            }) ?? throw new InvalidOperationException("Cannot deserialize section 'cache', please ensure that this section is correct");
            return services.AddSingleton(Options.Create(cacheOptions));
        }

        private static IServiceCollection RegisterDatabaseOptions(this IServiceCollection services, Dictionary<string, object> appsettings)
        {
            var databaseOptionsJsonElement = (JsonElement)(appsettings["database"]
                ?? throw new InvalidOperationException("Cannot find section 'database', please ensure that this file exists"));
            var databaseOptions = databaseOptionsJsonElement.Deserialize<DatabaseOptions>(new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            }) ?? throw new InvalidOperationException("Cannot deserialize section 'database', please ensure that this section is correct");
            if (string.IsNullOrWhiteSpace(databaseOptions.ConnectionString))
            {
                throw new InvalidOperationException("Check in 'database' section 'connectionString' if is correct");
            }
            services.AddSingleton(Options.Create(databaseOptions));
            return services;
        }

        // metoda wprowadzona aby umożliwić dostęp do serwisów podczas rejestrowania w kontenerze IoC
        // podczas rejestracji sporej ilości serwisów może powodować opóźnienie w odpaleniu aplikacji
        internal static T GetService<T>(this IServiceCollection services)
             where T : notnull
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<T>();
        }

        #endregion
    }
}