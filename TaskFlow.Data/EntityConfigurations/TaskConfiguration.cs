using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlow.Models.Enums;

namespace TaskFlow.Data.EntityConfigurations;

public class TaskConfiguration : IEntityTypeConfiguration<Models.Entities.Task>
{
    public void Configure(EntityTypeBuilder<Models.Entities.Task> builder)
    {
        builder.ToTable("Tasks").HasKey(t => t.Id);

        builder.Property(t => t.Id)
               .HasColumnName("Id")
               .IsRequired();

        builder.Property(t => t.Title)
               .HasColumnName("Title")
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(t => t.Description)
               .HasColumnName("Description")
               .HasMaxLength(500);

        builder.Property(t => t.OperationTypeId)
               .HasColumnName("OperationTypeId")
               .IsRequired();

        builder.Property(t => t.AnalystId)
               .HasColumnName("AnalystId")
               .IsRequired();

        builder.Property(t => t.DeveloperId)
               .HasColumnName("DeveloperId");

        builder.Property(t => t.Status)
               .HasColumnName("Status")
               .IsRequired()
               .HasDefaultValue(AssignmentStatus.Pending)
               .HasConversion<int>();

        builder.Property(t => t.CreatedDate)
               .HasColumnName("CreatedDate")
               .IsRequired();

        builder.Property(t => t.AssignedDate)
               .HasColumnName("AssignedDate");

        builder.Property(t => t.CompletedDate)
               .HasColumnName("CompletedDate");

        builder.HasOne(t => t.OperationType)
               .WithMany(o => o.Tasks)
               .HasForeignKey(t => t.OperationTypeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Analyst)
               .WithMany() 
               .HasForeignKey(t => t.AnalystId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Developer)
               .WithMany() 
               .HasForeignKey(t => t.DeveloperId)
               .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(t => t.Status);
        builder.HasIndex(t => t.AssignedDate);
        builder.HasIndex(t => new { t.Status, t.OperationTypeId });
    }
}