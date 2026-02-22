namespace TaskFlow.Business.Interfaces;

public interface ITaskAssignmentStrategy
{
    int SelectDeveloper(Models.Entities.Task task, Dictionary<int, decimal> workloadScores);
}
