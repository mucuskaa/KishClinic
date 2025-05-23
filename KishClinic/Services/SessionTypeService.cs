using KishClinic.Data;
using KishClinic.Entities;
using Microsoft.EntityFrameworkCore;

namespace KishClinic.Services
{
    public class SessionTypeService : ISessionTypeService
    {
        private readonly KishClinicDbContext _context;

        public SessionTypeService(KishClinicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SessionType>> GetAllAsync()
        {
            return await _context.SessionTypes.ToListAsync();
        }

        public async Task<SessionType?> GetByIdAsync(int id)
        {
            return await _context.SessionTypes.FindAsync(id);
        }

        public async Task<SessionType> CreateAsync(SessionType sessionType)
        {
            _context.SessionTypes.Add(sessionType);
            await _context.SaveChangesAsync();
            return sessionType;
        }

        public async Task<SessionType?> UpdateAsync(int id, SessionType sessionType)
        {
            var existing = await _context.SessionTypes.FindAsync(id);
            if (existing == null) return null;

            existing.Description = sessionType.Description;
            existing.DefaultFee = sessionType.DefaultFee;
            existing.DefaultDuration = sessionType.DefaultDuration;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sessionType = await _context.SessionTypes.FindAsync(id);
            if (sessionType == null) return false;

            _context.SessionTypes.Remove(sessionType);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
