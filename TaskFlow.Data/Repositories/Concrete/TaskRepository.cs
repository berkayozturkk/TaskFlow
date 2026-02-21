using Microsoft.EntityFrameworkCore;
using TaskFlow.Data.Context;
using TaskFlow.Data.Repositories.Interfaces;
using TaskFlow.Models.Enums;

namespace TaskFlow.Data.Repositories;

public class TaskRepository : GenericRepository<TaskFlow.Models.Entities.Task>, ITaskRepository
{
    public TaskRepository(AppDbContext context) : base(context) {}

    public async Task<IEnumerable<Models.Entities.Task>> GetAssignedTasksAsync()
    {
        return await _dbSet
        .Include(t => t.OperationType)
        .Include(t => t.Analyst)
        .ThenInclude(a => a.Role)
        .Include(t => t.Developer)
        .ThenInclude(d => d.Role)
        .Where(t => t.DeveloperId != null
                    && (t.Status == AssignmentStatus.Assigned
                        || t.Status == AssignmentStatus.InProgress))
        .OrderByDescending(t => t.AssignedDate)
        .AsNoTracking()
        .ToListAsync();
    }

    public async Task<IEnumerable<Models.Entities.Task>> GetFilteredTasksAsync(int? analystId = null, int? developerId = null, AssignmentStatus? status = null, int? difficulty = null)
    {
        var query = _dbSet
         .Include(t => t.OperationType)
         .Include(t => t.Analyst)
         .Include(t => t.Developer)
         .AsNoTracking()  
         .AsQueryable();

        if(analystId.HasValue)
        query = query.Where(t => t.AnalystId == analystId.Value);

        if (developerId.HasValue)
            query = query.Where(t => t.DeveloperId == developerId.Value);

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        if (difficulty.HasValue && Enum.IsDefined(typeof(TaskDifficulty), difficulty.Value))
        {
            var difficultyEnum = (TaskDifficulty)difficulty.Value;
            query = query.Where(t => t.OperationType.DifficultyLevel == difficultyEnum);
        }

        return await query.OrderByDescending(t => t.CreatedDate).ToListAsync();
    }

    public async Task<IEnumerable<Models.Entities.Task>> GetPendingTasksWithoutDifficultyAsync()
    {
        return await _dbSet
           .Include(t => t.OperationType)
           .Include(t => t.Analyst)
           .Include(t => t.Developer)
           .Where(t => t.Status == AssignmentStatus.Pending)
           .OrderByDescending(t => t.CreatedDate)
           .AsNoTracking()
           .ToListAsync();
    }

    public async Task<IEnumerable<Models.Entities.Task>> GetTasksByStatusAsync(AssignmentStatus status)
    {
        return await _dbSet
        .Include(t => t.OperationType)
        .Include(t => t.Analyst)
        .ThenInclude(a => a.Role)
        .Include(t => t.Developer)
        .ThenInclude(d => d.Role)
        .Where(t => t.Status == status)
        .OrderByDescending(t => t.CreatedDate)
        .AsNoTracking() 
        .ToListAsync();
    }

    public async Task<IEnumerable<Models.Entities.Task>> GetUnassignedTasksAsync()
    {
        return await _dbSet
         .Include(t => t.OperationType)
         .Include(t => t.Analyst)
         .ThenInclude(a => a.Role)
         .Where(t => t.Status == AssignmentStatus.Pending
                     && t.DeveloperId == null                    
                     && t.OperationTypeId != 0)                  
         .OrderByDescending(t => t.CreatedDate)
         .AsNoTracking()
         .ToListAsync();
    }
}
