using Microsoft.EntityFrameworkCore;
using Aryap.Data.Entities;

namespace Aryap.Core
{
    public class DbDatabaseContext : DbContext
    {
        public DbDatabaseContext(DbContextOptions<DbDatabaseContext> options) : base(options)
        {
        }

        // DbSet properties map to database tables
        public DbSet<User> Users { get; set; }
        public DbSet<Cloth> Clothes { get; set; }
        public DbSet<Cart> Cart { get; set; }

        
    }
}