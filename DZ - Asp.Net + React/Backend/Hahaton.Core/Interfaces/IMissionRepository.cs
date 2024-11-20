using Hahaton.Core.Dtos;
using Hahaton.Core.Models;

namespace Hahaton.Core.Interfaces;

public interface IMissionRepository
{
    Task<List<Mission>?> GetAllAsync();
    Task<Mission> CreateAsync(Mission mission, string username);
    Task<Mission?> UpdateAsync(UpdateMissionDto missionDto,int id);
    Task<Mission?> DeleteAsync(int id);
    Task<Mission?> CompleteAsync(int id);
    Task<IEnumerable<Mission>> GetMissionsByUsername(string username);
}