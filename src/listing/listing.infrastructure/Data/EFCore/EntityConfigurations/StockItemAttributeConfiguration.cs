using listing.core.Domain.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace listing.infrastructure.Data.EFCore.EntityConfigurations;

public class StockItemAttributeConfiguration : IEntityTypeConfiguration<StockItemAttribute>
{
	public void Configure(EntityTypeBuilder<StockItemAttribute> builder)
	{
		builder.Ignore(e => e.DomainEvents);

		builder.ToTable("stock_item_attributes");
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnName("_id"); ;

		builder.Property(e => e.StockItemId).HasColumnName("stock_item_id");

		builder.Property(e => e.AttributeId).IsRequired().HasColumnName("attribute_id");
		builder.Property(e => e.AttributeValueId).IsRequired().HasColumnName("attribute_value_id");
	}
}
