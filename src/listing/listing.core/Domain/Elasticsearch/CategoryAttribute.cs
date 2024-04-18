using listing.core.Domain.Elasticsearch.SeedWork;
using System.Text.Json.Serialization;

namespace listing.core.Domain.Elasticsearch;

public class CategoryAttribute :ESEntity
{
	public CategoryAttribute(int categoryId, int attributeId, string name, bool required, bool varianter, bool slicer, bool allowCustom)
	{
		CategoryId = categoryId;
		AttributeId = attributeId;
		Name = name;
		Required = required;
		Varianter = varianter;
		Slicer = slicer;
		AllowCustom = allowCustom;
	}

		[JsonPropertyName("categoryId")]
		public int CategoryId { get; set; }

		[JsonPropertyName("attributeId")]
		public int AttributeId { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; } = null!;

		[JsonPropertyName("required")]
		public bool Required { get; set; }

        [JsonPropertyName("varianter")]
		public bool Varianter { get; set; }

		[JsonPropertyName("slicer")]
        public bool Slicer { get; set; }

        [JsonPropertyName("allowCustom")]
		public bool AllowCustom { get; set; }    
}