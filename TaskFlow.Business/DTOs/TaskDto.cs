using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Business.DTOs;

public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string OperationTypeName { get; set; } = string.Empty;
    public int DifficultyLevel { get; set; }
    public string Status { get; set; } = string.Empty;
    public string AnalystName { get; set; } = string.Empty;
    public string? DeveloperName { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? AssignedDate { get; set; }
    public DateTime? CompletedDate { get; set; }

    public string CreatedDateFormatted => CreatedDate.ToString("dd.MM.yyyy HH:mm");
    public string AssignedDateFormatted => AssignedDate?.ToString("dd.MM.yyyy HH:mm") ?? "-";
    public string CompletedDateFormatted => CompletedDate?.ToString("dd.MM.yyyy HH:mm") ?? "-";

    public string StatusClass => Status switch
    {
        "Bekliyor" => "badge bg-warning",
        "Atandı" => "badge bg-info",
        "Devam Ediyor" => "badge bg-primary",
        "Tamamlandı" => "badge bg-success",
        "İptal" => "badge bg-danger",
        _ => "badge bg-secondary"
    };
}
