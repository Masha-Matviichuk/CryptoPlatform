using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Api.Configuration
{
    public static class LoggerConfig
    {
        public static WebApplicationBuilder RegisterLogger(this WebApplicationBuilder builder)
        {

            var logger = new LoggerConfiguration().Enrich.FromLogContext()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
                .WriteTo.File("logs.txt")
                .CreateLogger();


            builder.Logging.AddSerilog(logger);
            return builder;
        }
    }
}