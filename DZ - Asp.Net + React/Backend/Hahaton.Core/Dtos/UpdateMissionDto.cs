using Hahaton.Core.Enums;

namespace Hahaton.Core.Dtos;

public class UpdateMissionDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public int PeriodOfTime { get; set; }

    public DateTime? EndDate { get; set; } = null;
    
    public Complexity Complexity { get; set; }
    public Priority Priority { get; set; }
}