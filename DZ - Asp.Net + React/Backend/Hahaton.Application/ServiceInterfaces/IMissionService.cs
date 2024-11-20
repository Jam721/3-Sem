using Hahaton.Core.Models;

namespace Hahaton.Application.ServiceInterfaces;

public interface IMissionService
{
    Task<int> GetCountMissions(List<Mission>? missions);

    List<DayMissions> GetDaysOfWeekWithMissions(List<Mission> missions);

    int GetIso8601WeekOfYear(DateTime time);
}