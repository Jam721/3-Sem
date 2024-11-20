using System.ComponentModel.DataAnnotations;
using Hahaton.Core;
using Hahaton.Core.Enums;

namespace Hahaton.API.Contracts.Mission;

public class MissionDataRequest
{
    [Required]
    public int Id { get; set; }
    [Required]
    public Complexity Complexity { get; set; }
    [Required]
    public Priority Priority { get; set; }

    [Required]
    public DateTime? EndDate { get; set; }
    [Required]
    public DateTime PeriodOfTime { get; set; }

}