namespace Hahaton.Application.ServiceInterfaces;

public interface IUserService
{
    Task Register(string userName, string email, string password);
    Task<string> Login(string userName, string password);
}