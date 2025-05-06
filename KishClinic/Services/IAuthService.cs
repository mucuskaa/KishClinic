using KishClinic.Entities;
using KishClinic.Models;

namespace KishClinic.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(RegisterDto request);
        Task<string?> LoginAsync(LoginDto request);
    }
}
 