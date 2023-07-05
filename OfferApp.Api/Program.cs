using Microsoft.Extensions.Options;
using OfferApp.Core;
using OfferApp.Infrastructure;
using OfferApp.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCore();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (args.Contains("--RunMigrations"))
{
    Console.WriteLine("Running migrations...");
    using var scope = app.Services.CreateScope();
    var databaseOptions = scope.ServiceProvider.GetRequiredService<IOptions<DatabaseOptions>>();
    databaseOptions.Value.RunMigrationsOnStart = true;
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    await dbInitializer.Start();
    return;
}
// Configure the HTTP request pipeline.

app.UseInfrastructure();

app.Run();
