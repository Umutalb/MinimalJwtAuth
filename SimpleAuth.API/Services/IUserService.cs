using SimpleAuth.API.Entities;

namespace SimpleAuth.API.Services
{
    public interface IUserService
    {
        User? GetByEmail(string email);
        User Register(string email, string password);
        User? Login(string email, string password);
    }
}
