using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ancanet.server.Enums;

namespace ancanet.server.Models.Configurations
{
    public class ProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            //builder.HasKey(x => x.AppUserId);

            builder
                .Property(e => e.Gender)
                .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<Gender>(v));
        }
    }
}
