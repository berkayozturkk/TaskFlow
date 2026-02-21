using TaskFlow.Business.DTOs;
using TaskFlow.Service.DTOs;

namespace TaskFlow.Business.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAllTasksAsync();
    Task<IEnumerable<TaskDto>> GetFilteredTasksAsync(TaskFilterDto filter);
}

