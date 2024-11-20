namespace Hahaton.Core.Models;

public class DayMissions
{
    public string Day { get; set; } = string.Empty; // День недели
    public List<Mission> Missions { get; set; } = new List<Mission>(); // Задачи для этого дня
}