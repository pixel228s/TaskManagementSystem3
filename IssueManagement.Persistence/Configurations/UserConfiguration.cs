using IssueManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IssueManagement.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100).IsFixedLength();

            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.CreatedOn).HasColumnType("datetime");

            builder.Property(x => x.UserName).IsRequired();

            builder.Property(x => x.Email).HasColumnType("nvarchar");


            builder.Ignore(user => user.ConcurrencyStamp);

            builder.Ignore(user => user.PhoneNumber);

            builder.Ignore(user => user.PhoneNumberConfirmed);

            builder.Ignore(user => user.TwoFactorEnabled);

            builder.Ignore(user => user.LockoutEnabled);

            builder.Ignore(user => user.LockoutEnd);

            builder.Ignore(user => user.AccessFailedCount);
        }
    }
}
