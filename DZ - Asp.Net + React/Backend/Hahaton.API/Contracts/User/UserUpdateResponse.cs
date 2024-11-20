using System.ComponentModel.DataAnnotations;

namespace Hahaton.API.Contracts.User;

public class UserUpdateResponse
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;
}