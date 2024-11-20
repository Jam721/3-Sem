using System.ComponentModel.DataAnnotations;
using Hahaton.Core.Enums;

namespace Hahaton.API.Contracts.Mission;

public class UpdateMissionRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public int PeriodOfTime { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [EndDateMustBeGreaterThanCreatedAt] // Добавьте атрибут валидации
    public DateTime? EndDate { get; set; }

    [Required]
    public Complexity Complexity { get; set; }

    [Required]
    public Priority Priority { get; set; }
}