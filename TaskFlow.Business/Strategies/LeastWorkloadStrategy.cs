using TaskFlow.Business.Interfaces;
using TaskFlow.Models.Entities;

public class LeastWorkloadStrategy : ITaskAssignmentStrategy
{
    public int SelectDeveloper(TaskFlow.Models.Entities.Task task,Dictionary<int, decimal> workloadScores)
    {
        return workloadScores.MinBy(x => x.Value).Key;
    }
}
