using TaskFlow.Business.Interfaces;
using TaskFlow.Data.Repositories.Interfaces;

namespace TaskFlow.Business.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
}


