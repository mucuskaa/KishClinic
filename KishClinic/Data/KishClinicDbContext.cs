using KishClinic.Entities;
using Microsoft.EntityFrameworkCore;

namespace KishClinic.Data
{
    public class KishClinicDbContext(DbContextOptions<KishClinicDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }

    }
}
