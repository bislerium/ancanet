using FluentEmail.Core.Interfaces;
using FluentEmail.Smtp;
using System.Net.Mail;

namespace ancanet.server.Email.FluentEmail
{
    public static class ConfiguredFluentEmailExtensions
    {
        public static void AddConfiguredFluentEmail(this IServiceCollection services,
            ConfigurationManager configuration, bool isTest = false)
        {            
            if (isTest) 
            {
                var localTestSettings = configuration.GetSection("EmailSettings:LocalTestSettings");
                var defaultFromEmail = localTestSettings["DefaultFromEmail"];
                var host = localTestSettings["Host"];
                var port = localTestSettings.GetValue<int>("Port");

                services.AddFluentEmail(defaultFromEmail);
                // I'm using dev mode using 'smtp4dev' hence i'm only using host and port
                services.AddSingleton<ISender>(x => new SmtpSender(new SmtpClient(host, port))); 
            } 
            else
            {
                var productionSettings = configuration.GetSection("EmailSettings:ProductionSettings");
                var defaultFromEmail = productionSettings["DefaultFromEmail"];
                var username = productionSettings["Username"];
                var password = productionSettings["Password"];
                var host = productionSettings["Host"];
                var port = productionSettings.GetValue<int>("Port");

                services.AddFluentEmail(defaultFromEmail)
                    .AddSmtpSender(host, port, username, password);
            }
        }
    }
}
