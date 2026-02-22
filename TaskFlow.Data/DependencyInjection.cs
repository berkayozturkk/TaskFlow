using Microsoft.Extensions.DependencyInjection;
using TaskFlow.Data.Context;
using TaskFlow.Data.Repositories.Interfaces;
using TaskFlow.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TaskFlow.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddData(
       this IServiceCollection services)
    {
        //Uygulama ayağa kalkarken In-Memory veritabanı oluştuurlur.
        services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TaskDistributionDB"));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IOperationTypeRepository, OperationTypeRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();

        return services;
    }
}
