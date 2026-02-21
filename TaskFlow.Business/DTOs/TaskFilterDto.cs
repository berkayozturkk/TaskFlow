using TaskFlow.Models.Enums;

namespace TaskFlow.Service.DTOs;

public class TaskFilterDto
{
    public int? AnalystId { get; set; }

    public int? DeveloperId { get; set; }

    public AssignmentStatus? Status { get; set; }

    public int? Difficulty { get; set; }

    /// <summary>
    /// Başlık veya açıklamada arama yapmak için (opsiyonel)
    /// </summary>
    public string? SearchTerm { get; set; }
 
}