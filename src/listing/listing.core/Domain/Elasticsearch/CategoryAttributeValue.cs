using listing.core.Domain.Elasticsearch.SeedWork;
using System.Text.Json.Serialization;

namespace listing.core.Domain.Elasticsearch;

public class CategoryAttributeValue :ESEntity
{
	public CategoryAttributeValue(int categoryId, int attributeId, int attributeValueId, string name)
	{
		CategoryId = categoryId;
		AttributeId = attributeId;
		AttributeValueId = attributeValueId;
		Name = name;
	}

	[JsonPropertyName("categoryId")]
	public int CategoryId { get; set; }

	[JsonPropertyName("attributeId")]
	public int AttributeId { get; set; }

	[JsonPropertyName("attributeValueId")]
	public int AttributeValueId { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;
}