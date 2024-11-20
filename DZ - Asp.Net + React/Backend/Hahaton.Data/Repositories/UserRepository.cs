using Hahaton.Core.Dtos;
using Hahaton.Core.Interfaces;
using Hahaton.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Hahaton.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<User>?> GetAllAsync()
    {
        var users = await _dbContext.Users
            .AsNoTracking()
            .ToListAsync();

        return users;
    }

    public async Task<User?> GetByUserNameAsync(string username)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserName == username);
        
        if (user == null) return null;
        
        return user;
    }

    public async Task<User?> Create(User user)
    {
        bool isValid = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName) == null;
        if (!isValid) throw new Exception("Юзер с таким именем уже существует");
        
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<List<Mission>> GetAllAsyncByUser(string username)
    {
        return await _dbContext.Missions.Where(m=>m.Username==username).ToListAsync();
    }

    public async Task<User?> Update(UserUpdateDto userUpdateDto, string username)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(m => m.UserName == username);

        if (user == null) return null;

        user.UserName = userUpdateDto.UserName;
        user.Email = userUpdateDto.Email;

        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<User?> Delete(string username)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(m => m.UserName == username);

        if (user == null) return null;

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<User?> GetUser(string username)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
        if (user == null) throw new Exception("Нет такого юзера");

        return user;
    }
}