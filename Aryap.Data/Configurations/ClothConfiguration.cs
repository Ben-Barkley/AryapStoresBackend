using Aryap.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aryap.Data.Configurations
{
    internal class ClothConfiguration : IEntityTypeConfiguration<Cloth>
    {
        public void Configure(EntityTypeBuilder<Cloth> builder)
        {
            builder.ToTable("Clothes");
            builder.HasKey(e => e.ClothId);
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Description).IsRequired(false);
            builder.Property(e => e.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.ImageUrl).IsRequired(false);
            builder.Property(e => e.Stock).IsRequired();
            builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}