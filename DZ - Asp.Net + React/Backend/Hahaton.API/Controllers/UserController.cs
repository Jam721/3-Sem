using System.IdentityModel.Tokens.Jwt;
using Hahaton.API.Contracts.User;
using Hahaton.Application.ServiceInterfaces;
using Hahaton.Core.Dtos;
using Hahaton.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hahaton.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public UserController(IUserRepository userRepository, IUserService userService)
    {
        _userRepository = userRepository;
        _userService = userService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userRepository.GetAllAsync();

        return Ok(users);
    }

    [Authorize]
    [HttpGet("GetUsername")]
    public Task<IActionResult> GetUsername()
    {
        var token = Request.Cookies["tasty"];
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenRead = tokenHandler.ReadJwtToken(token);
        var username = tokenRead.Claims.FirstOrDefault(c=>c.Type=="username")!.Value;

        return Task.FromResult<IActionResult>(Ok(username));
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserRegisterResponse registerResponse)
    {
        await _userService.Register(registerResponse.UserName, registerResponse.Email, registerResponse.Password);
        return Ok();
    }
    
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserLoginResponse loginResponse)
    {
        try
        {
            // Проверяем, правильно ли выполняется метод Login в UserService
            var token = await _userService.Login(loginResponse.UserName, loginResponse.Password);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            // Сохраняем токен в куки
            Response.Cookies.Append("tasty", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict
            });

            return Ok(new { token });
        }
        catch (Exception e)
        {
            // Логирование ошибки
            Console.WriteLine($"Login error: {e.Message}");
            return Unauthorized(new { message = e.Message });
        }
    }



    // [Authorize]
    // [HttpPut("Redact")]
    // public async Task<IActionResult> Redact(UserUpdateResponse updateResponse)
    // {
    //     var token = Request.Cookies["tasty"];
    //     var tokenHandler = new JwtSecurityTokenHandler();
    //     var tokenRead = tokenHandler.ReadJwtToken(token);
    //     var username = tokenRead.Claims.FirstOrDefault(c=>c.Type=="username")!.Value;
    //     
    //     var myUser = await _userRepository.GetUser(username);
    //     if (myUser == null) return BadRequest("Нет юзера");
    //     
    //     myUser.UserName = updateResponse.UserName;
    //     myUser.Email = updateResponse.Email;
    //     
    //     var newToken = _userService.Login(username, myUser.PasswordHash).Result;
    //
    //     Response.Cookies.Append("tasty", newToken);
    //     
    //     var userResponse = new UserUpdateDto()
    //     {
    //         Email = updateResponse.Email,
    //         UserName = updateResponse.UserName
    //     };
    //
    //     
    //     var user = await _userRepository.Update(userResponse, username);
    //
    //     if (user == null) BadRequest("Не найден пользователь");
    //
    //     return Ok(user);
    // }

    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUser()
    {
        var token = Request.Cookies["tasty"];
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenRead = tokenHandler.ReadJwtToken(token);
        var username = tokenRead.Claims.FirstOrDefault(c=>c.Type=="username")!.Value;
        
        var myUser = await _userRepository.GetUser(username);
        return Ok(myUser);
    }
    
    [HttpDelete("Delete/{username}")]
    public async Task<IActionResult> Delete([FromRoute]string username)
    {
        var user = await _userRepository.Delete(username);

        if (user == null) BadRequest("Не найден пользователь");

        return Ok(user);
    }
}