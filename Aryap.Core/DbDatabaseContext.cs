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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // Configure Cloth entity
            modelBuilder.Entity<Cloth>(entity =>
            {
                entity.ToTable("Clothes");
                entity.HasKey(e => e.ClothId);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Description).IsRequired(false);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.ImageUrl).IsRequired(false);
                entity.Property(e => e.Stock).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // Configure Cart entity
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");
                entity.HasKey(e => e.CartItemId);

                // Relationships
                entity.HasOne(c => c.User)
                      .WithMany()
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.Cloth)
                      .WithMany()
                      .HasForeignKey(c => c.ClothId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Quantity).IsRequired().HasDefaultValue(1);
            });
        }
    }
}