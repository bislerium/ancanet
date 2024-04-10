using ancanet.server.Email.FluentEmail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace ancanet.server.Email.IdentityEmail
{
    public class IdentityEmailSender([FromKeyedServices(ServiceLifetime.Transient)] IEmailService emailService) : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await emailService.SendAsync(new EmailMessageModel
            {
                ToAddress = email,
                Subject = subject,
                Body = htmlMessage
            });
        }
    }

    public static class Injection
    {
        public static IServiceCollection AddIdentityEmailSender(this IServiceCollection services)
        {
            return services.AddTransient<IEmailSender, IdentityEmailSender>();
        }
    }
}
