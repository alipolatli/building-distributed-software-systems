using listing.core.Domain.EFCore.SeedWork;

namespace listing.core.Domain.EFCore;

public class SaleItemMedia :EFEntity
{
	public int SalesItemId { get; set; }

	public string Url { get; set; } = null!;
    public int Type { get; set; }
}
