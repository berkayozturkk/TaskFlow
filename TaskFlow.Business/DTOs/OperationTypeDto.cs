namespace TaskFlow.Business.DTOs;

public class OperationTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int DifficultyLevel { get; set; }
}
