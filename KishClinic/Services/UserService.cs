using KishClinic.Data;
using KishClinic.Entities;
using KishClinic.Services;

public class UserService : IUserService
{
    private readonly KishClinicDbContext _context;

    public UserService(KishClinicDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }
}
