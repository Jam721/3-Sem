using Hahaton.Core.Dtos;
using Hahaton.Core.Interfaces;
using Hahaton.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Hahaton.Data.Repositories;

public class MissionRepository : IMissionRepository
{
    private readonly AppDbContext _dbContext;

    public MissionRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Mission>?> GetAllAsync()
    {
        var missions = await _dbContext.Missions
            .AsNoTracking()
            .ToListAsync();

        return missions;
    }

    public async Task<Mission> CreateAsync(Mission mission, string username)
    {
        mission.Username = username;
        
        await _dbContext.Missions.AddAsync(mission);
        await _dbContext.SaveChangesAsync();

        return mission;
    }

    public async Task<Mission?> UpdateAsync(UpdateMissionDto missionDto,int id)
    {
        var mission = await _dbContext.Missions
            .FirstOrDefaultAsync(m => m.Id == id);

        if (mission == null) return null;
        
        mission.Title = missionDto.Title;
        mission.Description = missionDto.Description;
        mission.PeriodOfTime = missionDto.PeriodOfTime;
        mission.EndDate = missionDto.EndDate;
        mission.Complexity = missionDto.Complexity;
        mission.Priority = missionDto.Priority;

        await _dbContext.SaveChangesAsync();

        return mission;
    }

    public async Task<Mission?> CompleteAsync(int id)
    {
        var mission = await _dbContext.Missions
            .FirstOrDefaultAsync(m => m.Id == id);

        if (mission == null) return null;

        mission.Completed = true;
        
        await _dbContext.SaveChangesAsync();

        return mission;
    }

    public async Task<IEnumerable<Mission>> GetMissionsByUsername(string username)
    {
        return await _dbContext.Missions
            .Where(m => m.Username == username) // Предполагаем, что у вас есть поле Username в модели Mission
            .ToListAsync();
    }

    public async Task<Mission?> DeleteAsync(int id)
    {
        var mission = await _dbContext.Missions
            .FirstOrDefaultAsync(m => m.Id == id);

        if (mission == null) return null;

        _dbContext.Missions.Remove(mission);
        await _dbContext.SaveChangesAsync();

        return mission;
    }
}