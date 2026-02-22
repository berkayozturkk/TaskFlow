using TaskFlow.Business.DTOs;
using TaskFlow.Models.Entities;
using TaskFlow.Models.Enums;

public static class WorkloadCalculator
{
    public static Dictionary<int, decimal> Calculate(
        IEnumerable<EmployeeDto> developers,
        IEnumerable<TaskFlow.Models.Entities.Task> assignedTasks)
    {
        var taskGroups = assignedTasks
            .GroupBy(t => t.DeveloperId)
            .ToDictionary(g => g.Key, g => g.ToList());

        var workloadScores = new Dictionary<int, decimal>();

        foreach (var dev in developers)
        {
            var devTasks = taskGroups.ContainsKey(dev.Id)
                ? taskGroups[dev.Id]
                : new List<TaskFlow.Models.Entities.Task>();

            decimal score = devTasks.Sum(t =>
                1 + GetDifficultyValue(t.OperationType));

            workloadScores[dev.Id] = score;
        }

        return workloadScores;
    }

    private static int GetDifficultyValue(OperationType? operationType)
    {
        if (operationType?.DifficultyLevel == null)
            return 1;

        return (int)operationType.DifficultyLevel;
    }
}
