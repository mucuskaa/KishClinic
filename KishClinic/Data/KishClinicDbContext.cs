using KishClinic.Entities;
using Microsoft.EntityFrameworkCore;

namespace KishClinic.Data
{
    public class KishClinicDbContext(DbContextOptions<KishClinicDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<SessionType> SessionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserSession>()
                .HasKey(us => new { us.UserId, us.SessionId });

            modelBuilder.Entity<SessionType>()
                .Property(st => st.DefaultFee)
                .HasPrecision(10, 2);
        }
    }
}
