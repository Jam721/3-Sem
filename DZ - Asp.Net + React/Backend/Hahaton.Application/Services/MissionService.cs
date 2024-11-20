using System.Globalization;
using Hahaton.Application.ServiceInterfaces;
using Hahaton.Core.Models;

namespace Hahaton.Application.Services;

public class MissionService : IMissionService
{
    public async Task<int> GetCountMissions(List<Mission>? missions)
    {
        if (missions == null) return 1;
        
        var mission = missions.OrderByDescending(m => m.Id).FirstOrDefault();

        if (mission == null) return 1;
        
        return mission.Id + 1;
    }
    
    public List<DayMissions> GetDaysOfWeekWithMissions(List<Mission> missions)
    {
        // Задаем правильный порядок дней недели
        var daysOfWeekOrder = new List<DayOfWeek>
        {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday,
            DayOfWeek.Saturday,
            DayOfWeek.Sunday
        };

        // Словарь для перевода дней недели на русский
        var dayOfWeekTranslations = new Dictionary<DayOfWeek, string>
        {
            { DayOfWeek.Monday, "Понедельник" },
            { DayOfWeek.Tuesday, "Вторник" },
            { DayOfWeek.Wednesday, "Среда" },
            { DayOfWeek.Thursday, "Четверг" },
            { DayOfWeek.Friday, "Пятница" },
            { DayOfWeek.Saturday, "Суббота" },
            { DayOfWeek.Sunday, "Воскресенье" }
        };

        var dayMissions = new List<DayMissions>();

        // Перебираем дни недели в правильном порядке
        foreach (var day in daysOfWeekOrder)
        {
            var missionsForDay = missions.Where(m => m.CreatedAt.DayOfWeek == day).ToList();
            dayMissions.Add(new DayMissions
            {
                Day = dayOfWeekTranslations[day],  // Переводим день недели на русский
                Missions = missionsForDay
            });
        }

        return dayMissions;
    }


    
    public int GetIso8601WeekOfYear(DateTime time)
    {
        return ISOWeek.GetWeekOfYear(time);
    }
}