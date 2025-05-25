using Aryap.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aryap.Data.Configurations
{
    internal class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Cart");
            builder.HasKey(e => e.CartItemId);

            // Relationships
            builder.HasOne(c => c.User)
                  .WithMany()
                  .HasForeignKey(c => c.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Cloth)
                  .WithMany()
                  .HasForeignKey(c => c.ClothId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Quantity).IsRequired().HasDefaultValue(1);
        }
    }
}