using BoutiqueHotel.entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueHotel.data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(m => m.ProductId);
            builder.Property(m => m.Name).IsRequired().HasMaxLength(100);
            builder.Property(m => m.DateAdded).HasDefaultValueSql("getdate()");
        }
    }
}