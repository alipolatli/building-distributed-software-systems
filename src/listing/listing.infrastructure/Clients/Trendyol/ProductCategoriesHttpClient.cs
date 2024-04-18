using System.Net.Http.Json;

namespace listing.infrastructure.Clients.Trendyol
{
	#region Request
	public class ProductCategoriesHttpClient(HttpClient _httpClient)
	{
		public async Task<TrendyolCategoryRootObject> GetProductCategoriesAsync(CancellationToken cancellationToken = default)
		{
			string path = $"{_httpClient.BaseAddress}/product-categories";
			return (await _httpClient.GetFromJsonAsync<TrendyolCategoryRootObject>(path, cancellationToken))!;
		}
	}
	#endregion

	#region Response Object
	public class TrendyolCategoryRootObject
	{
		public IEnumerable<TrendyolCategory> categories { get; set; } = Enumerable.Empty<TrendyolCategory>();
	}

	public class TrendyolCategory
	{
		public int id { get; set; }
		public string name { get; set; } = null!;
		public int? parentId { get; set; }
		public IEnumerable<TrendyolCategory> subCategories { get; set; } = Enumerable.Empty<TrendyolCategory>();
	}
	#endregion
}
