using TaskFlow.Models.Base;
using TaskFlow.Models.Enums;

namespace TaskFlow.Models.Entities;

public class OperationType : BaseEntity
{ 
    public OperationType() { }

    public string Name { get; set; }             
    public string Description { get; set; }        
    public TaskDifficulty DifficultyLevel { get; set; }      

    public ICollection<Task>? Tasks { get; set; }
}
