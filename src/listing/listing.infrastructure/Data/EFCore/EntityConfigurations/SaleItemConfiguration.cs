using listing.core.Domain.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace listing.infrastructure.Data.EFCore.EntityConfigurations;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
	public void Configure(EntityTypeBuilder<SaleItem> builder)
	{
		builder.Ignore(e => e.DomainEvents);

		builder.ToTable("sale_items");
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnName("_id");

		builder.Property(e => e.Title).IsRequired().HasMaxLength(maxLength: 100).HasColumnName("title");
		builder.Property(e => e.Description).IsRequired().HasMaxLength(maxLength: 100).HasColumnName("description");


		builder.Property(e => e.CategoryId).IsRequired().HasColumnName("category_id");
		builder.Property(e => e.BrandId).IsRequired().HasColumnName("brand_id");
		builder.Property(e => e.ShippingId).IsRequired().HasColumnName("shipping_id");

		builder.HasMany(e => e.StockItems).WithOne().HasForeignKey(e => e.SaleItemId);
	}
}
