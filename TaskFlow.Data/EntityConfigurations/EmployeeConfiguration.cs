using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlow.Models.Entities;

namespace TaskFlow.Data.EntityConfigurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees").HasKey(e => e.Id);

        builder.Property(e => e.Id)
               .HasColumnName("Id")
               .IsRequired();

        builder.Property(e => e.FirstName)
               .HasColumnName("FirstName")
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(e => e.LastName)
               .HasColumnName("LastName")
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(e => e.Email)
               .HasColumnName("Email")
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(e => e.IsActive)
               .HasColumnName("IsActive")
               .IsRequired()
               .HasDefaultValue(true);

        builder.Property(e => e.RoleId)
               .HasColumnName("RoleId")
               .IsRequired();

        builder.HasIndex(e => e.Email, "UK_Employees_Email").IsUnique();

        builder.HasOne(e => e.Role)
               .WithMany(r => r.Employees)
               .HasForeignKey(e => e.RoleId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}