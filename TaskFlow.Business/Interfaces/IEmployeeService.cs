using TaskFlow.Business.DTOs;

namespace TaskFlow.Business.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
    Task<(IEnumerable<EmployeeDto> Analysts, IEnumerable<EmployeeDto> Developers)> GetAnalystsAndDevelopersAsync();
}

