using Microsoft.EntityFrameworkCore;
using System.Data;
using TaskFlow.Models.Entities;

namespace TaskFlow.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<OperationType> OperationTypes { get; set; }
    public DbSet<Models.Entities.Task> Tasks { get; set; }
}
