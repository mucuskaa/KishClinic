using KishClinic.Data;
using KishClinic.Entities;
using KishClinic.Services;
using Microsoft.EntityFrameworkCore;

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

    public async Task<User?> UpdateAsync(User user)
    {
        var existingUser = await _context.Users.FindAsync(user.Id);
        if (existingUser == null)
        {
            return null;
        }

        _context.Entry(existingUser).CurrentValues.SetValues(user);
        await _context.SaveChangesAsync();
        return existingUser;
    }
}
