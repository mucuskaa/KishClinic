using KishClinic.Entities;
using KishClinic.Models;

namespace KishClinic.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<string?> LoginAsync(UserDto request);
    }
}
