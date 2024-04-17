using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace listing.core.Domain.Elasticsearch;

public class CategoryAttributeValue
{
	public CategoryAttributeValue(int categoryId, int attributeId, int attributeValueId, string name)
	{
		CategoryId = categoryId;
		AttributeId = attributeId;
		AttributeValueId = attributeValueId;
		Name = name;
	}

	[Key]
	[JsonPropertyName("_id")]
	public string Id { get; set; }  = null!;

	[JsonPropertyName("categoryId")]
	public int CategoryId { get; set; }

	[JsonPropertyName("attributeId")]
	public int AttributeId { get; set; }

	[JsonPropertyName("attributeValueId")]
	public int AttributeValueId { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;
}