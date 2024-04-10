namespace ancanet.server.Email.FluentEmail
{
    public class EmailMessageModel
    {
        public required string ToAddress { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}
