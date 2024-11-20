using Hahaton.Core.Models;

namespace Hahaton.Application;

public interface IJwtProvider
{
    string GenerateToken(User user);
}