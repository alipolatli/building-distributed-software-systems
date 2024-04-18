using listing.core.Domain.Elasticsearch.SeedWork;
using System.Text.Json.Serialization;

namespace listing.core.Domain.Elasticsearch;
public class Category :ESEntity
{
	public Category(long categoryId, string name, int? parentId, string hierarchyName, string hierarchyId, bool available)
	{
		CategoryId = categoryId;
		Name = name;
		ParentId = parentId;
		HierarchyName = hierarchyName;
		HierarchyId = hierarchyId;
		Available = available;
	}

    [JsonPropertyName("categoryId")]
    public long CategoryId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("parentId")]
    public int? ParentId { get; set; }

    [JsonPropertyName("hierarchyName")]
    public string HierarchyName { get; set; } = null!;

    [JsonPropertyName("hierarchyId")]
    public string HierarchyId { get; set; } = null!;

    [JsonPropertyName("available")]
    public bool Available { get; set; }
}