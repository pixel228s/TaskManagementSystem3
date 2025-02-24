using IssueManagement.Domain.Enums;
using IssueManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IssueManagement.Persistence.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasData(Enum.GetValues(typeof(StatusTypes))
                .Cast<StatusTypes>()
                .Select(s => new Status
                {
                    Id = (int)s,
                    Name = s.ToString(),
                }));
        }
    }
}
