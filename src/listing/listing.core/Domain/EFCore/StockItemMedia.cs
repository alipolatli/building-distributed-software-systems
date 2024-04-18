using listing.core.Domain.EFCore.SeedWork;

namespace listing.core.Domain.EFCore;

public class StockItemMedia :EFEntity, ITenancy
{
	public int StockItemId { get; set; }

	public string Url { get; set; } = null!;
    public int Type { get; set; }
}
