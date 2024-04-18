using System.Net.Http.Json;

namespace listing.infrastructure.Clients.Trendyol
{
	#region Request
	public class AttributesHttpClient(HttpClient _httpClient)
	{
		public async Task<TrendyolAttributeRootObject> GetAttributesAsync(int categoryId, CancellationToken cancellationToken = default)
		{
			string path = $"{_httpClient.BaseAddress}/product-categories/{categoryId}/attributes";
			return (await _httpClient.GetFromJsonAsync<TrendyolAttributeRootObject>(path, cancellationToken))!;
		}
	}
	#endregion

	#region Response
	public class TrendyolAttributeRootObject
	{
		public int id { get; set; }
		public string name { get; set; } = null!;
		public string displayName { get; set; } = null!;
		public IEnumerable<TrendyolAttribute> categoryAttributes { get; set; } = Enumerable.Empty<TrendyolAttribute>();
	}

	public class TrendyolAttribute
	{
		public int categoryId { get; set; }
		public TrendyolAttributeContent attribute { get; set; } = null!;
		public bool required { get; set; }
		public bool allowCustom { get; set; }
		public bool varianter { get; set; }
		public bool slicer { get; set; }
		public IEnumerable<TrendyolAttributeValue> attributeValues { get; set; } = Enumerable.Empty<TrendyolAttributeValue>();
	}

	public class TrendyolAttributeContent
	{
		public int id { get; set; }
		public string name { get; set; } = null!;
	}

	public class TrendyolAttributeValue
	{
		public int id { get; set; }
		public string name { get; set; } = null!;
	}
	#endregion
}