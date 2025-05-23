using KishClinic.Entities;

namespace KishClinic.Services
{
    public interface ISessionTypeService
    {
        Task<IEnumerable<SessionType>> GetAllAsync();
        Task<SessionType?> GetByIdAsync(int id);
        Task<SessionType> CreateAsync(SessionType sessionType);
        Task<SessionType?> UpdateAsync(int id, SessionType sessionType);
        Task<bool> DeleteAsync(int id);
    }
}
