using TaskFlow.Business.DTOs;
using TaskFlow.Models.Enums;
using TaskFlow.Service.DTOs;

namespace TaskFlow.Business.Interfaces;

public interface ITaskService
{
    Task<TaskDto> GetByIdAsync(int id);
    Task<IEnumerable<TaskDto>> GetAllTasksAsync();
    Task<IEnumerable<TaskDto>> GetFilteredTasksAsync(TaskFilterDto filter);
    Task<IEnumerable<TaskDto>> GetTasksByStatusAsync(AssignmentStatus status);
    Task<IEnumerable<TaskDto>> GetPendingTasksWithoutDifficultyAsync();
    Task UpdateTaskOperationTypeAsync(int taskId, int operationTypeId);
    Task CreateTaskAsync(CreateTaskDto createTask);
}

