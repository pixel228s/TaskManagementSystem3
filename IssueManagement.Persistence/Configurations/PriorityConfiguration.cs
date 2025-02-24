using IssueManagement.Domain.Enums;
using IssueManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IssueManagement.Persistence.Configurations
{
    public class PriorityConfiguration : IEntityTypeConfiguration<Priority>
    {
        public void Configure(EntityTypeBuilder<Priority> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasData(Enum.GetValues(typeof(PriorityTypes))
                .Cast<PriorityTypes>()
                .Select(s => new Priority
                {
                    Id = (int)s,
                    Name = s.ToString(),
                }));
        }
    }
}
