using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Business.DTOs;

public class CreateTaskDto
{
    [Required(ErrorMessage = "Başlık alanı zorunludur")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Başlık 3-50 karakter arasında olmalıdır")]
    [Display(Name = "Başlık")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Açıklama alanı zorunludur")]
    [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
    [Display(Name = "Açıklama")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Analist seçimi zorunludur")]
    [Display(Name = "Analist")]
    public int AnalystId { get; set; }

    [Required(ErrorMessage = "İşlem tipi seçimi zorunludur")]
    [Display(Name = "İşlem Tipi")]
    public int OperationTypeId { get; set; }
}
