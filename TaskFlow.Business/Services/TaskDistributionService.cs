using TaskFlow.Business.Interfaces;
using TaskFlow.Models.Enums;

namespace TaskFlow.Business.Services;

public class TaskDistributionService : ITaskDistributionService
{
    private readonly ITaskService _taskService;
    private readonly IEmployeeService _employeeService;

    public TaskDistributionService(ITaskService taskService,IEmployeeService employeeService)
    {
        _taskService = taskService;
        _employeeService = employeeService;
    }

    public async Task<bool> SmartAssignTasksAsync()
    {
        // ADIM 1: Atanmamış görevleri entity olarak al 
        var unassignedTasks = await _taskService.GetUnassignedTaskEntitiesAsync();
        if (!unassignedTasks.Any())
            return false;

        // ADIM 2: Developerları getir
        var developers = await _employeeService.GetDevelopersAsync();
        if (!developers.Any())
            return false;

        // ADIM 3: Aktif görevleri entity olarak al
        var assignedTasks = await _taskService.GetAssignedTaskEntitiesAsync();

        // ADIM 4: Developer skorlarını hesapla
        var workloadScores = new Dictionary<int, decimal>();

        foreach (var dev in developers)
        {
            var devTasks = assignedTasks.Where(t => t.DeveloperId == dev.Id);
            decimal score = devTasks.Count() + devTasks.Sum(t =>
            {
                if (t.OperationType?.DifficultyLevel == null)
                    return 1; 
                return (int)t.OperationType.DifficultyLevel;
            });

            workloadScores[dev.Id] = score;
        }

        // ADIM 5: Görevleri sırala ve ata
        var sortedTasks = unassignedTasks.OrderBy(t => t.CreatedDate);

        foreach (var task in sortedTasks)
        {
            var bestDevId = workloadScores.MinBy(x => x.Value).Key;

            // Task entity'sini güncelle
            task.DeveloperId = bestDevId;
            task.AssignedDate = DateTime.Now;
            task.Status = AssignmentStatus.Assigned;

            await _taskService.UpdateTaskAsync(task);  

            int taskDifficulty = task.OperationType?.DifficultyLevel != null
             ? Convert.ToInt32(task.OperationType.DifficultyLevel)
             : 1;
            workloadScores[bestDevId] += (1 + taskDifficulty);
        }

        return true;
    }
}
