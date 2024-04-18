using listing.core.Domain.EFCore.SeedWork;

namespace listing.core.Domain.EFCore;

public class SaleItem : EFEntity, ITenancy, IAggregateRoot
{
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    public int ShippingId { get; set; }

    public string Title { get; private set; } = null!;
	public string Description { get; private set; } = null!;

    public IEnumerable<StockItem> StockItems { get; set; } = Enumerable.Empty<StockItem>();
}