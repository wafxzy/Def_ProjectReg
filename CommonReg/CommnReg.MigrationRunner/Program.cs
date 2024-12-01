
using CommonReg.MigrationRunner.Interfaces;
using CommonReg.MigrationRunner.Realizations;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

static void CreateLogger()
{
    string outputTemplate = "{Timestamp:yyyy-MM-dd} | {Timestamp:HH:mm:ss.fff} | {Timestamp:zzz} | {Level:u3} | {Message:lj}{NewLine}{Exception}";

    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console(outputTemplate: outputTemplate)
        .CreateLogger();
}

static IServiceProvider CreateServiceProvider()
{
    return new ServiceCollection()
        .AddScoped<IDatabaseMigrator, DatabaseMigrator>()
        .BuildServiceProvider();
}

static void ExecuteMigrations(
    long? targetVersion
    )
{
    IServiceProvider serviceProvider = CreateServiceProvider();
    using IServiceScope scope = serviceProvider.CreateScope();

    IDatabaseMigrator migrator = scope.ServiceProvider.GetRequiredService<IDatabaseMigrator>();

    migrator.MigrateDatabase(targetVersion);
}

static long? ParseTargetVersion(
    string[] args
    )
{
    if (args.Length < 1)
    {
        return null;
    }

    ArgumentException argumentException = new(null, nameof(args));

    if (args.Length > 1)
    {
        throw argumentException;
    }

    if (!long.TryParse(args[0], out long result))
    {
        throw argumentException;
    }

    return result;
}// See https://aka.ms/new-console-template for more information

try
{
    CreateLogger();

    Log.Logger.Information("Started with args: {args}", args);

    long? targetVersion = ParseTargetVersion(args);

    if (targetVersion.HasValue)
    {
        Log.Logger.Information("Starting migration DOWN to the version [{version}]", targetVersion);
    }
    else
    {
        Log.Logger.Information("Starting migration UP to the latest version");
    }

    ExecuteMigrations(targetVersion);

    return 0;
}
catch (Exception ex)
{
    Log.Logger.Error(ex, "An error occurred while migrating or initializing the databases.");
    return -1;
}
