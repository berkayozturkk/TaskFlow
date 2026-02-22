using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlow.Models.Entities;

namespace TaskFlow.Data.EntityConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles").HasKey(r => r.Id);

        builder.Property(r => r.Id)
               .HasColumnName("Id")
               .IsRequired();

        builder.Property(r => r.Name)
               .HasColumnName("Name")
               .IsRequired()
               .HasMaxLength(50);

        builder.HasIndex(r => r.Name, "UK_Roles_Name").IsUnique();

        builder.HasMany(r => r.Employees)
               .WithOne(e => e.Role)
               .HasForeignKey(e => e.RoleId);
    }
}