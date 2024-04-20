using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ancanet.server.Models
{
    [Table("Messages")]
    public class Message
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MinLength(1)]
        public string Content { get; set; } = null!;

        [Required]
        public DateTimeOffset SentAt { get; set; } = DateTimeOffset.UtcNow;

        public string SentById { get; set; } = null!;

        public UserProfile SentBy { get; set; } = null!;

        public string SentToId { get; set; } = null!;

        public UserProfile SentTo { get; set; } = null!;
    }
}
