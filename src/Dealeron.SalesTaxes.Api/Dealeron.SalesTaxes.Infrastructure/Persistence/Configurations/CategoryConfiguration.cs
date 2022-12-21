using Dealeron.SalesTaxes.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dealeron.SalesTaxes.Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(c => c.CategoryId);
            builder.Property(c => c.Code).HasColumnName("Code").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Name).HasColumnName("Name").HasMaxLength(255).IsRequired();
            builder.Property(c => c.TaxToApply).HasConversion<double>().IsRequired().HasDefaultValue(0.1);
            builder.HasMany(c => c.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);
        }
    }
}
