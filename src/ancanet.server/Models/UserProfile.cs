using ancanet.server.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ancanet.server.Models
{
    [Table("UserProfiles")]
    public class UserProfile
    {
        [Key]
        [Column("Id")]
        public string AppUserId { get; set; } = null!;

        public AppUser AppUser { get; set; } = null!;

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        public virtual ICollection<Message> SentMessages { get; set; } = [];

        public virtual ICollection<Message> ReceivedMessages { get; set; } = [];
    }
}
