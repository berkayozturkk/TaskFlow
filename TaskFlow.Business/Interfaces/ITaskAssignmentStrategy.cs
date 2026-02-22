namespace TaskFlow.Business.Interfaces;

public interface ITaskAssignmentStrategy
{
    Task<int> SelectDeveloperAsync(Models.Entities.Task task, Dictionary<int, decimal> workloadScores,
       IEnumerable<Models.Entities.Task> assignedTasks);
}

