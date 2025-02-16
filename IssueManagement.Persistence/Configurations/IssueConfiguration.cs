using IssueManagement.Domain.Enums;
using IssueManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            builder.Property(x => x.Status).HasDefaultValue(Status.ToDo);
            builder.Property(x => x.Priority).HasDefaultValue(Priority.Low);

            builder.HasOne(u => u.AssignedUser)
                .WithMany(i => i.Issues)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
