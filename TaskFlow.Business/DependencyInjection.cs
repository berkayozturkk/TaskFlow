using Microsoft.Extensions.DependencyInjection;
using TaskFlow.Business.Interfaces;
using TaskFlow.Business.Services;

namespace TaskFlow.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusiness(
       this IServiceCollection services)
    {
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IOperationTypeService, OperationTypeService>();
        services.AddScoped<ITaskDistributionService, TaskDistributionService>();
        services.AddSingleton<ITaskAssignmentStrategy, LeastWorkloadStrategy>();

        return services;
    }
}
