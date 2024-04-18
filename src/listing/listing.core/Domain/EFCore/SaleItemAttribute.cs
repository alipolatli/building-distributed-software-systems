using listing.core.Domain.EFCore.SeedWork;

namespace listing.core.Domain.EFCore;

public class SaleItemAttribute :EFEntity
{
    public int SalesItemId { get; set; }

    public int AttributeId { get; set; }
    public int AttributeValueId { get; set; }
}
