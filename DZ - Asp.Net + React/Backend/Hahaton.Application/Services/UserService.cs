using Hahaton.Application.Auth;
using Hahaton.Application.ServiceInterfaces;
using Hahaton.Core.Interfaces;
using Hahaton.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Hahaton.Application.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _provider;

    public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtProvider provider)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _provider = provider;
    }
    
    public async Task Register(string userName, string email, string password)
    {
        var hashedPassword = _passwordHasher.Generate(password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            Email = email,
            PasswordHash = hashedPassword
        };

        await _userRepository.Create(user);
    }

    public async Task<string> Login(string userName, string password)
    {
        var user = await _userRepository.GetByUserNameAsync(userName);

        var result = _passwordHasher.Verify(password, user!.PasswordHash);

        if (result == false) throw new Exception("Не удалось залогиниться");

        var token = _provider.GenerateToken(user);

        return token;
    }
}