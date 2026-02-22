using TaskFlow.Business.Interfaces;

public class LeastWorkloadStrategy : ITaskAssignmentStrategy
{
    public async Task<int> SelectDeveloperAsync( TaskFlow.Models.Entities.Task task,
        Dictionary<int, decimal> workloadScores,
        IEnumerable<TaskFlow.Models.Entities.Task> assignedTasksTask)
    {
        var assignedTasks = assignedTasksTask.ToList();

        var newDifficulty = (int)task.OperationTypeId;

        var candidateDevelopers = workloadScores.Keys
            .Where(devId => !IsSequentialDifficulty(devId, newDifficulty, assignedTasks))
            .ToList();

        if (!candidateDevelopers.Any())
            candidateDevelopers = workloadScores.Keys.ToList();

        return candidateDevelopers
            .OrderBy(devId => workloadScores[devId])
            .First();
    }

    private bool IsSequentialDifficulty( int developerId, int newDifficulty,
        List<TaskFlow.Models.Entities.Task> assignedTasks)
    {
        var lastTask = assignedTasks.Where(t => t.DeveloperId == developerId)
            .OrderByDescending(t => t.AssignedDate)
            .FirstOrDefault();

        if (lastTask == null)
            return false;

        var lastDifficulty = (int)lastTask.OperationTypeId;

        return Math.Abs(lastDifficulty - newDifficulty) <= 1;
    }
}
