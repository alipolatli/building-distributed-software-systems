using listing.core.Domain.EFCore.SeedWork;

namespace listing.core.Domain.EFCore;

public class StockItem : EFEntity, ITenancy
{
    public int SaleItemId { get; set; }

    public string SKU { get;  set; } = null!;
	public string GTIN { get;  set; } = null!;
	public string? ASIN { get;  set; }

	public decimal ListPrice { get;  set; }
	public decimal DiscountRate { get;  set; }
	public decimal VatRate { get;  set; }

    public long Quantity { get; set; }

    public IEnumerable<StockItemAttribute> Attributes { get;  set; } = Enumerable.Empty<StockItemAttribute>();
	public IEnumerable<StockItemMedia>? Medias { get; set; } = Enumerable.Empty<StockItemMedia>();
}