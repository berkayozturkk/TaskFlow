namespace TaskFlow.Business.Interfaces;

public interface ITaskDistributionService
{
    Task<bool> SmartAssignTasksAsync();
}
