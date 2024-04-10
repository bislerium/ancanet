using ancanet.server.Email.FluentEmail;

namespace ancanet.server.Email
{
    public interface IEmailService
    {
        /// <summary>
        /// Send an email.
        /// </summary>
        /// <param name="emailMessage">Message object to be sent</param>
        Task SendAsync(EmailMessageModel emailMessage);
    }
}
