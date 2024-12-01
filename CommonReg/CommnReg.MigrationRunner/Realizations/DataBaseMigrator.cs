using CommonReg.Common.Helpers;
using CommonReg.MigrationRunner.Helpers;
using CommonReg.MigrationRunner.Interfaces;
using CommonReg.MigrationRunner.Realizations.Conventions;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Conventions;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.MigrationRunner.Realizations
{
    public  class DatabaseMigrator : IDatabaseMigrator
    {
        private static ServiceProvider GetServiceProvider(
       string connectionString
       )
        {
            return new ServiceCollection()
                 .AddFluentMigratorCore()
                 .AddSingleton<IConventionSet>(new NamingConventionSet())
                 .ConfigureRunner(rb => rb
                     .AddPostgres()
                     .WithGlobalConnectionString(connectionString)
                     .WithGlobalCommandTimeout(TimeSpan.FromMinutes(30))
                     .WithVersionTable(new VersionTable())
                     .ScanIn(MigrationHelper.MigrationAssembly)
                        .For.Migrations())
                     .AddLogging(lb => lb.AddFluentMigratorConsole())
                  .BuildServiceProvider(false);
        }

        public static void MigrateDatabase(
        string connectionString,
        long? version = null)
        {
            using ServiceProvider serviceProvider = GetServiceProvider(connectionString);
            IMigrationRunner runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            if (version.HasValue)
            {
                runner.MigrateDown(version.Value);
            }
            else if (runner.HasMigrationsToApplyUp())
            {
                runner.MigrateUp();
            }
        }

        public void MigrateDatabase(long? version)
        {
            Log.Logger.Information("Trying migrating database");

            string connectionString = ConnectionStringHelper.GenerateConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                Log.Logger.Error("Connection string is null or empty");
                return;
            }

            try
            {
                MigrateDatabase(connectionString, version);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Failed to migrate database");
                throw;
            }
        }
    }
}
