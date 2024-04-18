using listing.core.Domain.EFCore.SeedWork;

namespace listing.core.Domain.EFCore;

public class SaleItem : EFEntity
{
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    public int ShippingId { get; set; }

    public string SKU { get; private set; } = null!;
    public string GTIN { get; private set; } = null!;
    public string? ASIN { get; private set; }

    public string Title { get; private set; } = null!;
	public string Description { get; private set; } = null!;
    
    public decimal ListPrice { get; private set; }
    public decimal DiscountRate { get; private set; }
    public decimal VatRate { get; private set; }

    public IEnumerable<SaleItemAttribute>? Attributes { get; set; }
	public IEnumerable<SaleItemMedia>? Medias { get; set; }
}
