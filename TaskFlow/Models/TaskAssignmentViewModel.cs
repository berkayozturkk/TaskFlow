using TaskFlow.Business.DTOs;

namespace TaskFlow.Models;

public class TaskAssignmentViewModel
{
    public IEnumerable<TaskDto> PendingTasks { get; set; } = new List<TaskDto>();
    public IEnumerable<TaskDto> AssignedTasks { get; set; } = new List<TaskDto>();
}
