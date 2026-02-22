using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlow.Models.Entities;

namespace TaskFlow.Data.EntityConfigurations;

public class OperationTypeConfiguration : IEntityTypeConfiguration<OperationType>
{
    public void Configure(EntityTypeBuilder<OperationType> builder)
    {
        builder.ToTable("OperationTypes").HasKey(o => o.Id);

        builder.Property(o => o.Id)
               .HasColumnName("Id")
               .IsRequired();

        builder.Property(o => o.Name)
               .HasColumnName("Name")
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(o => o.Description)
               .HasColumnName("Description")
               .HasMaxLength(200);

        builder.Property(o => o.DifficultyLevel)
               .HasColumnName("DifficultyLevel")
               .IsRequired()
               .HasConversion<int>(); // Enum'u int olarak sakla

        builder.HasIndex(o => o.Name, "UK_OperationTypes_Name").IsUnique();

        builder.HasMany(o => o.Tasks)
               .WithOne(t => t.OperationType)
               .HasForeignKey(t => t.OperationTypeId);
    }
}