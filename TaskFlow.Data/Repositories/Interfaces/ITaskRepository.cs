using TaskFlow.Models.Enums;

namespace TaskFlow.Data.Repositories.Interfaces;

public interface ITaskRepository : IGenericRepository<Models.Entities.Task>
{
    Task<IEnumerable<Models.Entities.Task>> GetFilteredTasksAsync(
            int? analystId = null,
            int? developerId = null,
            AssignmentStatus? status = null,
            int? difficulty = null);
    Task<IEnumerable<Models.Entities.Task>> GetTasksByStatusAsync(AssignmentStatus status);
    Task<IEnumerable<Models.Entities.Task>> GetPendingTasksWithoutDifficultyAsync();
}

