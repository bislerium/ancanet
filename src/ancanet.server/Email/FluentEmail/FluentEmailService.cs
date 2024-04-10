using FluentEmail.Core;

namespace ancanet.server.Email.FluentEmail
{
    public class FluentEmailService(ILogger<FluentEmailService> logger, IFluentEmailFactory fluentEmailFactory) : IEmailService
    {
        public async Task SendAsync(EmailMessageModel emailMessageModel)
        {
            logger.LogInformation("Sending email");
            await fluentEmailFactory.Create().To(emailMessageModel.ToAddress)
                .Subject(emailMessageModel.Subject)                
                .Body(emailMessageModel.Body, true) // true means this is an HTML format message
                .SendAsync();
        }
    }

    public static class Injection
    {
        public static IServiceCollection AddScopedFluentEmailService(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddKeyedScoped<IEmailService, FluentEmailService>(ServiceLifetime.Scoped);
        }

        // Only for Identity Email Sender
        public static IServiceCollection AddTransientFluentEmailService(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddKeyedTransient<IEmailService, FluentEmailService>(ServiceLifetime.Transient);
        }
    }
}
