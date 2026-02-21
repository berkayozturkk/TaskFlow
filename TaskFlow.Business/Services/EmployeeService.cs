using TaskFlow.Business.Interfaces;
using TaskFlow.Data.Repositories.Interfaces;

namespace TaskFlow.Business.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
}
