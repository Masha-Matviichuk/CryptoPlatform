using BLL.Dtos;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Configuration
{
    public static class EmailConfig
    {
            public static WebApplicationBuilder RegisterEmailService(this WebApplicationBuilder builder)
            {
                var mainEmailConfiguration = new MailSettings()
                {
                    MainEmail = builder.Configuration["MainEmail:Name"],
                    MainPassword = builder.Configuration["MainEmail:Password"],
                    MainDisplayName = "Crypto Platform",
                    MainHost = builder.Configuration["MainEmail:Host"],
                    MainPort = builder.Configuration["MainEmail:Port"]
                };
                
                var middleEmailConfiguration = new MailSettings()
                {
                    MainEmail = builder.Configuration["MiddleEmail:Name"],
                    MainPassword = builder.Configuration["MiddleEmail:Password"],
                    MainDisplayName = "Message from customer",
                    MainHost = builder.Configuration["MiddleEmail:Host"],
                    MainPort = builder.Configuration["MiddleEmail:Port"]
                };

                builder.Services.AddSingleton(mainEmailConfiguration);
                builder.Services.AddSingleton(middleEmailConfiguration);
    
                builder.Services.AddScoped<IEmailService, EmailService>();
                return builder;
            }
    }
}