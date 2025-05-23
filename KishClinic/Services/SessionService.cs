using KishClinic.Data;
using KishClinic.Entities;
using Microsoft.EntityFrameworkCore;

namespace KishClinic.Services
{
    public class SessionService : ISessionService
    {
        private readonly KishClinicDbContext _context;

        public SessionService(KishClinicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Session>> GetAllAsync()
        {
            return await _context.Sessions
                .Include(s => s.SessionType)
                .ToListAsync();
        }

        public async Task<Session?> GetByIdAsync(int id)
        {
            return await _context.Sessions
                .Include(s => s.SessionType)
                .FirstOrDefaultAsync(s => s.ID == id);
        }
        public async Task<Session> CreateAsync(Session session)
        {
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            var userSession = new UserSession
            {
                UserId = session.ClientID,
                SessionId = session.ID
            };
            _context.UserSessions.Add(userSession);
            await _context.SaveChangesAsync();

            return await _context.Sessions
                .Include(s => s.SessionType)
                .FirstOrDefaultAsync(s => s.ID == session.ID);
        }

        public async Task<Session?> UpdateAsync(int id, Session session)
        {
            var existingSession = await _context.Sessions.FindAsync(id);
            if (existingSession == null)
            {
                return null;
            }

            existingSession.ClientID = session.ClientID;
            existingSession.SessionDate = session.SessionDate;
            existingSession.StartTime = session.StartTime;
            existingSession.FinishTime = session.FinishTime;
            existingSession.Status = session.Status;
            existingSession.Notes = session.Notes;
            existingSession.SessionTypeID = session.SessionTypeID;

            await _context.SaveChangesAsync();
            return existingSession;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return false;
            }

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
