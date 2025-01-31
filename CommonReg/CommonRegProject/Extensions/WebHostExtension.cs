using CommonReg.Common.JWTToken.Options;
using Serilog;
using Serilog.Events;

namespace CommonReg.API.Extensions
{

    public static class WebHostExtension
    {
        public static void UseLoggingSerilog(this ConfigureHostBuilder webHost, IConfiguration configuration)
        {
            string outputTemplate = "{Timestamp:yyyy-MM-dd} | {Timestamp:HH:mm:ss.fff} | {Timestamp:zzz} | {SourceContext} | {Level:u3} | {Message:lj}{NewLine}{Exception}";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.Console(outputTemplate: outputTemplate)
                .WriteTo.Sentry(o =>
                {
                    o.Dsn = configuration.GetSection(SentryOption.SENTRY_OPTION)[SentryOption.DSN_KEY];

                    o.MinimumEventLevel = Enum.Parse<LogEventLevel>(
                        configuration.GetSection(SentryOption.SENTRY_OPTION)[SentryOption.MINIMUM_EVENT_LEVEL]);
                })
                .CreateLogger();

            webHost.UseSerilog(Log.Logger, true);
        }
    }
}