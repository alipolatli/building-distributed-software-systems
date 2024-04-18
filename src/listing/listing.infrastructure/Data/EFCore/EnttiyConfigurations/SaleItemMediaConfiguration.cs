using listing.core.Domain.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace listing.infrastructure.Data.EFCore.EnttiyConfigurations;

public class SaleItemMediaConfiguration : IEntityTypeConfiguration<SaleItemMedia>
{
	public void Configure(EntityTypeBuilder<SaleItemMedia> builder)
	{
		builder.Ignore(e => e.DomainEvents);

		builder.ToTable("sale_item_medias");
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnName("_id"); ;

		builder.Property(e => e.SalesItemId).HasColumnName("sale_item_id");

		builder.Property(e => e.Url).IsRequired().HasMaxLength(200).HasColumnName("url");
		builder.Property(e => e.Type).IsRequired().HasColumnName("type");
	}
}
