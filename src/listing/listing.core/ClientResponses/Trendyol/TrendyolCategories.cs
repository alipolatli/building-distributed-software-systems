namespace listing.core.ClientResponses.Trendyol;

public class TrendyolCategory
{
	public int id { get; set; }
	public string name { get; set; } = null!;
	public int? parentId { get; set; }
	public List<TrendyolCategory> subCategories { get; set; } = new();
}