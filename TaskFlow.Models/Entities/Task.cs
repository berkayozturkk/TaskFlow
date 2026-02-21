using TaskFlow.Models.Base;

namespace TaskFlow.Models.Entities;

public class Task : BaseEntity
{
    public Task() { }

    public string Title { get; set; }              
    public string Description { get; set; }    
    public int OperationTypeId { get; set; }        
    public OperationType? OperationType { get; set; }
    public int AnalystId { get; set; }             
    public Employee? Analyst { get; set; }
    public int? DeveloperId { get; set; }           
    public Employee? Developer { get; set; }
    public TaskStatus Status { get; set; }   
    public DateTime CreatedDate { get; set; }
    public DateTime? AssignedDate { get; set; }
    public DateTime? CompletedDate { get; set; }
}
