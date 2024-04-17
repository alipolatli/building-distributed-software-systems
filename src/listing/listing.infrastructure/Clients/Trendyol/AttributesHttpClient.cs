using listing.core.ClientResponses.Trendyol;
using System.Net.Http.Json;

namespace listing.infrastructure.Clients.Trendyol
{
	public class AttributesHttpClient(HttpClient httpClient)
	{
		public async Task<TrendyolAttributeRootObject?> GetAttributes(int categoryId, CancellationToken cancellationToken = default)
		{
			return await httpClient.GetFromJsonAsync<TrendyolAttributeRootObject>($"product-categories/{categoryId}/attributes", cancellationToken);
		}
	}
}