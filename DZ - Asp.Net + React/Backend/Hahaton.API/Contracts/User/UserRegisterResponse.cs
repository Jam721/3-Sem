using System.ComponentModel.DataAnnotations;

namespace Hahaton.API.Contracts.User;

public class UserRegisterResponse
{
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;
}