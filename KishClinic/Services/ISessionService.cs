using KishClinic.Entities;

namespace KishClinic.Services
{
    public interface ISessionService
    {
        Task<IEnumerable<Session>> GetAllAsync();
        Task<Session?> GetByIdAsync(int id);
        Task<Session> CreateAsync(Session session);
        Task<Session?> UpdateAsync(int id, Session session);
        Task<bool> DeleteAsync(int id);
    }
}
