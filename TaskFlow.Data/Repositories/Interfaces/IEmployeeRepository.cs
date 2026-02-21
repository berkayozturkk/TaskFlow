using TaskFlow.Models.Entities;

namespace TaskFlow.Data.Repositories.Interfaces;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    Task<IEnumerable<Employee>> GetActiveEmployeesWithRolesAsync();
}
