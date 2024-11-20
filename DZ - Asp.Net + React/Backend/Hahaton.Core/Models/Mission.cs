using Hahaton.Core.Enums;

namespace Hahaton.Core.Models;

public class Mission
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public bool Completed { get; set; } = false;

    public DateTime CreatedAt { get; set; }
    public DateTime? EndDate { get; set; }

    public int PeriodOfTime { get; set; }

    public Complexity Complexity { get; set; }
    public Priority Priority { get; set; }

    public string Username { get; set; } = string.Empty;
}