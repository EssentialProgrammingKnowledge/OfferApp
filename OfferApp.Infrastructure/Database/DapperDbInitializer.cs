using FluentMigrator.Runner;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace OfferApp.Infrastructure.Database
{
    internal sealed class DapperDbInitializer : IDbInitializer
    {
        private readonly DatabaseOptions _databaseOptions;
        private readonly IMigrationRunner _migrationRunner;

        public DapperDbInitializer(IOptions<DatabaseOptions> databaseOptions, IMigrationRunner migrationRunner)
        {
            _databaseOptions = databaseOptions.Value;
            _migrationRunner = migrationRunner;
        }

        public Task Start()
        {
            if (!_databaseOptions.RunMigrationsOnStart)
            {
                return Task.CompletedTask;
            }

            CreateDatabaseIfNotExists(_databaseOptions.ConnectionString!);
            _migrationRunner.MigrateUp();
            return Task.CompletedTask;
        }

        private static void CreateDatabaseIfNotExists(string connectionString)
        {
            var connectionStringSplited = connectionString.Split(";");
            var connectionStringWithoutDb = connectionStringSplited.Where(str => !str.Contains("Database="))
                                .Aggregate((current, next) => current + ";" + next);
            var database = connectionStringSplited.SingleOrDefault(str => str.Contains("Database="))?.Split("Database=")[1];

            if (string.IsNullOrEmpty(database))
            {
                throw new InvalidOperationException("Invalid ConnectionString. There is no value for 'Database=' check it and try again");
            }

            using var conn = new MySqlConnection(connectionStringWithoutDb);
            using var cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = $"CREATE DATABASE IF NOT EXISTS `{database}`";
            cmd.ExecuteNonQuery();
        }
    }
}
