using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ancanet.server.Models.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(x => x.Content)                
                .IsUnicode(true);

            builder.HasOne(x => x.SentTo)
                .WithMany(y => y.SentMessages)
                .HasForeignKey(x => x.SentToId)
                .IsRequired();

            builder.HasOne(x => x.SentBy)
                .WithMany(y => y.ReceivedMessages)
                .HasForeignKey(x => x.SentById)
                .IsRequired();
        }
    }
}
