namespace Hahaton.Core.Models;

public class MissionGroup
{
    public string Week { get; set; } = string.Empty; // Название недели
    public List<DayMissions> Days { get; set; } = new List<DayMissions>(); // Список дней и задач
}