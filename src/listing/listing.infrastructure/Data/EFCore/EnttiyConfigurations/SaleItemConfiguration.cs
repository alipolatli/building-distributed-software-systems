using listing.core.Domain.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace listing.infrastructure.Data.EFCore.EnttiyConfigurations;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
	public void Configure(EntityTypeBuilder<SaleItem> builder)
	{
		builder.Ignore(e => e.DomainEvents);

		builder.ToTable("sale_items");
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnName("_id");

		builder.Property(e => e.SKU).IsRequired().HasMaxLength(100).HasColumnName("sku");
		builder.HasAlternateKey(e => e.SKU);

		builder.Property(e => e.GTIN).IsRequired().HasMaxLength(40).HasColumnName("gtin");
		builder.HasAlternateKey(e => e.SKU);

		builder.Property(e => e.ASIN).IsRequired(false).HasMaxLength(maxLength: 40).HasColumnName("asin");
		builder.HasAlternateKey(e => e.SKU);


		builder.Property(e => e.Title).IsRequired().HasMaxLength(maxLength: 100).HasColumnName("title");
		builder.Property(e => e.Description).IsRequired().HasMaxLength(maxLength: 100).HasColumnName("description");

		builder.Property(e => e.ListPrice).IsRequired().HasPrecision(18, 4).HasColumnName("list_price");
		builder.Property(e => e.DiscountRate).IsRequired().HasPrecision(4, 2).HasColumnName("discount_rate");
		builder.Property(e => e.VatRate).IsRequired().HasPrecision(4, 2).HasColumnName("vat_rate");

		builder.Property(e => e.CategoryId).IsRequired().HasColumnName("category_id");
		builder.Property(e => e.BrandId).IsRequired().HasColumnName("brand_id");
		builder.Property(e => e.ShippingId).IsRequired().HasColumnName("shipping_id");

		builder.HasMany(e => e.Attributes).WithOne().HasForeignKey(e => e.SalesItemId);
		builder.HasMany(e => e.Medias).WithOne().HasForeignKey(e => e.SalesItemId);
	}
}
