using listing.core.ClientResponses.Trendyol;
using System.Net.Http.Json;

namespace listing.infrastructure.Clients.Trendyol
{
	public class ProductCategoriesHttpClient(HttpClient httpClient)
	{
		public async Task<IEnumerable<TrendyolCategory>?> GetProductCategories(CancellationToken cancellationToken = default)
		{
			return await httpClient.GetFromJsonAsync<IEnumerable<TrendyolCategory>>("product-categories",cancellationToken);
		}
	}
}
