using TaskFlow.Data.Context;
using TaskFlow.Data.Repositories.Interfaces;

namespace TaskFlow.Data.Repositories;

public class TaskRepository : GenericRepository<TaskFlow.Models.Entities.Task>, ITaskRepository
{
    public TaskRepository(AppDbContext context) : base(context) {}
}
