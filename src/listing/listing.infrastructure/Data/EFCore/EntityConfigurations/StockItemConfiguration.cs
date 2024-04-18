using listing.core.Domain.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace listing.infrastructure.Data.EFCore.EntityConfigurations;

public class StockItemConfiguration : IEntityTypeConfiguration<StockItem>
{
	public void Configure(EntityTypeBuilder<StockItem> builder)
	{
		builder.Ignore(e => e.DomainEvents);

		builder.ToTable("stock_items");
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnName("_id");

		builder.Property(e => e.SKU).IsRequired().HasMaxLength(100).HasColumnName("sku");
		builder.HasAlternateKey(e => e.SKU);

		builder.Property(e => e.GTIN).IsRequired().HasMaxLength(40).HasColumnName("gtin");
		builder.HasAlternateKey(e => e.SKU);

		builder.Property(e => e.ASIN).IsRequired(false).HasMaxLength(maxLength: 40).HasColumnName("asin");
		builder.HasAlternateKey(e => e.SKU);

		builder.Property(e => e.ListPrice).IsRequired().HasPrecision(18, 4).HasColumnName("list_price");
		builder.Property(e => e.DiscountRate).IsRequired().HasPrecision(4, 2).HasColumnName("discount_rate");
		builder.Property(e => e.VatRate).IsRequired().HasPrecision(4, 2).HasColumnName("vat_rate");

		builder.HasMany(e => e.Attributes).WithOne().HasForeignKey(e => e.StockItemId);
		builder.HasMany(e => e.Medias).WithOne().HasForeignKey(e => e.StockItemId);
	}
}
