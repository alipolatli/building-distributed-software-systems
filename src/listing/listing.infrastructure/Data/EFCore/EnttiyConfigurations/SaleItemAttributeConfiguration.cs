using listing.core.Domain.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace listing.infrastructure.Data.EFCore.EnttiyConfigurations;

public class SaleItemAttributeConfiguration : IEntityTypeConfiguration<SaleItemAttribute>
{
	public void Configure(EntityTypeBuilder<SaleItemAttribute> builder)
	{
		builder.Ignore(e => e.DomainEvents);

		builder.ToTable("sale_item_attributes");
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnName("_id"); ;

		builder.Property(e => e.SalesItemId).HasColumnName("sale_item_id");

		builder.Property(e => e.AttributeId).IsRequired().HasColumnName("attribute_id");
		builder.Property(e => e.AttributeValueId).IsRequired().HasColumnName("attribute_value_id");
	}
}
