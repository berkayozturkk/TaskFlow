using TaskFlow.Business.DTOs;
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

    public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
    {
        var employees = await _employeeRepository.GetActiveEmployeesWithRolesAsync();

        return employees.Select(e => new EmployeeDto
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Role = e.Role?.Name ?? "Bilinmiyor"
        });

    }

    public async Task<(IEnumerable<EmployeeDto> Analysts, IEnumerable<EmployeeDto> Developers)> GetAnalystsAndDevelopersAsync()
    {
        var employees = await _employeeRepository.GetActiveEmployeesWithRolesAsync();

        var analysts = employees
            .Where(e => e.Role != null && e.Role.Name == "Analist")
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Role = e.Role.Name
            });

        var developers = employees
            .Where(e => e.Role != null && e.Role.Name == "Developer")
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Role = e.Role.Name
            });

        return (analysts, developers);
    }

    public async Task<IEnumerable<EmployeeDto>> GetAnalystsAsync()
    {
        var employees = await _employeeRepository.GetActiveEmployeesWithRolesAsync();

        var analysts = employees
            .Where(e => e.Role != null && e.Role.Name == "Analist")
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName
            });

        return analysts;
    }

    public async Task<IEnumerable<EmployeeDto>> GetDevelopersAsync()
    {
        var employees = await _employeeRepository.GetActiveEmployeesWithRolesAsync();

        var analysts = employees
            .Where(e => e.Role != null && e.Role.Name == "Developer")
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName
            });

        return analysts;
    }
}
