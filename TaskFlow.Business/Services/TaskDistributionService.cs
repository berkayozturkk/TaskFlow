using TaskFlow.Business.Interfaces;
using TaskFlow.Models.Enums;

public class TaskDistributionService : ITaskDistributionService
{
    private readonly ITaskService _taskService;
    private readonly IEmployeeService _employeeService;
    private readonly ITaskAssignmentStrategy _assignmentStrategy;

    public TaskDistributionService( ITaskService taskService, IEmployeeService employeeService,
        ITaskAssignmentStrategy assignmentStrategy)
    {
        _taskService = taskService;
        _employeeService = employeeService;
        _assignmentStrategy = assignmentStrategy;
    }

    public async Task<bool> SmartAssignTasksAsync()
    {
        var unassignedTasks = await _taskService.GetUnassignedTaskEntitiesAsync();
        var developers = await _employeeService.GetDevelopersAsync();
        var assignedTasks = await _taskService.GetAssignedTaskEntitiesAsync();

        if (!unassignedTasks.Any() || !developers.Any())
            return false;

        var workloadScores = WorkloadCalculator.Calculate(developers, assignedTasks);

        var sortedTasks = unassignedTasks.OrderBy(t => t.CreatedDate);

        foreach (var task in sortedTasks)
        {
            var bestDevId = await _assignmentStrategy.SelectDeveloperAsync(task, workloadScores, assignedTasks);

            task.DeveloperId = bestDevId;
            task.AssignedDate = DateTime.Now;
            task.Status = AssignmentStatus.Assigned;

            await _taskService.UpdateTaskAsync(task);

            WorkloadCalculator.UpdateWorkload(workloadScores,bestDevId, task);
        }

        return true;
    }
}
