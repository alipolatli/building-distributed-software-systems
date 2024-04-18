using listing.core.Domain.EFCore.SeedWork;

namespace listing.core.Domain.EFCore;

public class StockItemAttribute :EFEntity, ITenancy
{
    public int StockItemId { get; set; }

    public int AttributeId { get; set; }
    public int AttributeValueId { get; set; }
}