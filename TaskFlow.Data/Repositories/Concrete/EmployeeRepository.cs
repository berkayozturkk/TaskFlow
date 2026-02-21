using Microsoft.EntityFrameworkCore;
using TaskFlow.Data.Context;
using TaskFlow.Data.Repositories.Interfaces;
using TaskFlow.Models.Entities;

namespace TaskFlow.Data.Repositories;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext context) : base(context) {}

    public async Task<IEnumerable<Employee>> GetActiveEmployeesWithRolesAsync()
    {
        return await _dbSet
            .Where(e => e.IsActive)
            .Include(e => e.Role)
            .OrderBy(e => e.FirstName)
            .AsNoTracking()
            .ToListAsync();
    }
}