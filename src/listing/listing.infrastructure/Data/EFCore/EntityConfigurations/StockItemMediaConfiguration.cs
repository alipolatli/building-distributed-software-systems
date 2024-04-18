using listing.core.Domain.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace listing.infrastructure.Data.EFCore.EntityConfigurations;

public class StockItemMediaConfiguration : IEntityTypeConfiguration<StockItemMedia>
{
	public void Configure(EntityTypeBuilder<StockItemMedia> builder)
	{
		builder.Ignore(e => e.DomainEvents);

		builder.ToTable("stock_item_medias");
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnName("_id"); ;

		builder.Property(e => e.StockItemId).HasColumnName("stock_item_id");

		builder.Property(e => e.Url).IsRequired().HasMaxLength(200).HasColumnName("url");
		builder.Property(e => e.Type).IsRequired().HasColumnName("type");
	}
}
