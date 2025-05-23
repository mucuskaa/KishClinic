using KishClinic.Entities;

public interface IUserService
{
    Task<User?> GetByIdAsync(int id);
}
