using System.IdentityModel.Tokens.Jwt;
using Hahaton.API.Contracts.Mission;
using Hahaton.Application.ServiceInterfaces;
using Hahaton.Core.Dtos;
using Hahaton.Core.Interfaces;
using Hahaton.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hahaton.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MissionController : ControllerBase
{
    private readonly IMissionRepository _repoMissionRepository;
    private readonly IMissionService _missionService;

    public MissionController(IMissionRepository repoMissionRepository, IMissionService missionService)
    {
        _repoMissionRepository = repoMissionRepository;
        _missionService = missionService;
    }

    [Authorize]
    [HttpGet("GetMissions")]
    public async Task<IActionResult> GetMissions()
    {
        var token = Request.Cookies["tasty"];
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenRead = tokenHandler.ReadJwtToken(token);
        var username = tokenRead.Claims.FirstOrDefault(c => c.Type == "username")!.Value;

        // Получаем только задачи текущего пользователя
        var missions = await _repoMissionRepository.GetMissionsByUsername(username);

        return Ok(missions);
    }

    
    
    [Authorize]
    [HttpGet("GetWeek")]
    public async Task<IActionResult> GetAllWeek()
    {
        var token = Request.Cookies["tasty"];
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenRead = tokenHandler.ReadJwtToken(token);
        var username = tokenRead.Claims.FirstOrDefault(c => c.Type == "username")!.Value;

        // Получаем все задачи пользователя
        var missions = await _repoMissionRepository.GetMissionsByUsername(username);

        if (missions == null || !missions.Any())
        {
            return NotFound("Нет задач.");
        }

        var groupedMissions = missions
            .GroupBy(m => _missionService.GetIso8601WeekOfYear(m.CreatedAt))
            .Select(g => new MissionGroup
            {
                Week = $"Неделя {g.Key}",
                Days = _missionService.GetDaysOfWeekWithMissions(g.ToList())
            })
            .OrderBy(mg => int.Parse(mg.Week.Split(' ')[1])) // Сортировка по неделям
            .ToList();

        return Ok(groupedMissions);
    }

    
    
    [HttpGet("GetAllMissionsData")]
    public async Task<IActionResult> GetAllData()
    {
        var missions = await _repoMissionRepository.GetAllAsync();
        
        if (missions == null) return BadRequest("Нет задач");
        
        var missionsData = missions.Select(m => new MissionDataRequest()
        {
            Id = m.Id,
            Complexity = m.Complexity,
            Priority = m.Priority,
            EndDate = m.EndDate,
            PeriodOfTime = DateTime.MinValue.Add((TimeSpan)(m.EndDate - DateTime.MinValue.AddMinutes(m.PeriodOfTime))!)
        });
        
        return Ok(missionsData);
    }

    [Authorize]
    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateMissionRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            Console.WriteLine($"Received request with title: {request.Title}, createdAt: {request.CreatedAt}, endDate: {request.EndDate}");

            var missions = await _repoMissionRepository.GetAllAsync();
            var lastId = await _missionService.GetCountMissions(missions);

            var mission = new Mission
            {
                Id = lastId,
                Complexity = request.Complexity,
                Title = request.Title,
                Completed = false,
                CreatedAt = request.CreatedAt,
                Description = request.Description,
                PeriodOfTime = request.PeriodOfTime,
                Priority = request.Priority,
                EndDate = request.EndDate
            };

            var token = Request.Cookies["tasty"];
            if (string.IsNullOrEmpty(token))
                return Unauthorized(new { message = "Token is missing." });

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenRead = tokenHandler.ReadJwtToken(token);
            var usernameClaim = tokenRead.Claims.FirstOrDefault(c => c.Type == "username");
            if (usernameClaim == null)
                return Unauthorized(new { message = "Username not found in token." });

            var username = usernameClaim.Value;
            Console.WriteLine($"Creating mission for user: {username}");

            var createdMission = await _repoMissionRepository.CreateAsync(mission, username);

            return Ok(createdMission);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating mission: {ex.Message}");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    
    [Authorize]
    [HttpPut("Update/{id:int}")]
    public async Task<IActionResult> Update(UpdateMissionRequest missionRequest, [FromRoute] int id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var token = Request.Cookies["tasty"];
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenRead = tokenHandler.ReadJwtToken(token);
        var username = tokenRead.Claims.FirstOrDefault(c => c.Type == "username")!.Value;

        var missions = await _repoMissionRepository.GetAllAsync();
        if (missions == null) return BadRequest("Нет задач");

        var isValid = missions.Any(m => m.Username == username && m.Id == id);
        if (!isValid) return BadRequest("Нет задач");
        
        if (missionRequest.EndDate < missionRequest.CreatedAt.AddMinutes(missionRequest.PeriodOfTime))
        {
            return BadRequest($"EndDate must be greater than CreatedAt + PeriodOfTime ({missionRequest.PeriodOfTime} minutes).");
        }

        var updateMission = new UpdateMissionDto()
        {
            Title = missionRequest.Title,
            Description = missionRequest.Description,
            PeriodOfTime = missionRequest.PeriodOfTime,
            Complexity = missionRequest.Complexity,
            Priority = missionRequest.Priority,
            EndDate = missionRequest.EndDate 
        };

        var mission = await _repoMissionRepository.UpdateAsync(updateMission, id);

        if (mission == null) return BadRequest("Не найдена задача");

        return Ok(mission);
    }


    [Authorize]
    [HttpDelete("Delete/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute]int id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var mission = await _repoMissionRepository.DeleteAsync(id);

        return Ok(mission);
    }
    
    [Authorize]
    [HttpPut("Complete/{id:int}")]
    public async Task<IActionResult> Update([FromRoute]int id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var mission = await _repoMissionRepository.CompleteAsync(id);

        return Ok(mission);
    }
}
