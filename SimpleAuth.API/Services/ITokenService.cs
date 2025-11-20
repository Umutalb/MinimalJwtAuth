using SimpleAuth.API.Entities;
using SimpleAuth.API.Models;

namespace SimpleAuth.API.Services
{
    public interface ITokenService
    {
        AuthResponse CreateToken(User user);
    }
}
