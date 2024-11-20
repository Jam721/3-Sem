using Hahaton.Core.Dtos;
using Hahaton.Core.Models;

namespace Hahaton.Core.Interfaces;

public interface IUserRepository
{
    Task<List<User>?> GetAllAsync();
    Task<User?> GetByUserNameAsync(string username);
    Task<User?> Create(User user);
    Task<User?> Update(UserUpdateDto userUpdateDto, string username);
    Task<User?> Delete(string username);
    Task<User?> GetUser(string username);
}