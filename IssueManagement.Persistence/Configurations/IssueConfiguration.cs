using IssueManagement.Domain.Enums;
using IssueManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IssueManagement.Persistence.Configurations
{
    public class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);   
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(4000);

            builder.Property(x => x.CompletionDate).HasColumnType("datetime");
            builder.Property(x => x.DueDate).HasColumnType("datetime");
            builder.Property(x => x.CreatedAt).HasColumnType("datetime");

            builder.Property(x => x.StatusId).HasDefaultValue((int)StatusTypes.ToDo);
            builder.Property(x => x.PriorityID).HasDefaultValue((int)PriorityTypes.Low);

            builder.HasOne(u => u.AssignedUser)
                .WithMany(i => i.Issues)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Status)
                .WithMany()
                .HasForeignKey(s => s.StatusId);

            builder.HasOne(p => p.Priority)
                .WithMany()
                .HasForeignKey(p => p.PriorityID);


        }
    }
}
