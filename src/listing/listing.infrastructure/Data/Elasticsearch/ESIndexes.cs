using listing.core.Domain.Elasticsearch;
using listing.core.Domain.Elasticsearch.SeedWork;

namespace listing.infrastructure.Data.Elasticsearch;

public static class ESIndexes
{
	private static readonly Dictionary<Type, string> IndexNameMap = new Dictionary<Type, string>
			{
				{ typeof(Category), "categories" },
				{ typeof(CategoryAttribute), "category-attributes" },
				{ typeof(CategoryAttributeValue), "category-attribute-values" },
			};

	public static string GetIndexName<T>() where T : ESEntity
	{
		var type = typeof(T);

		if (IndexNameMap.TryGetValue(type, out var indexName))
			return indexName;

		throw new NotSupportedException($"Index name not supported for type {type.Name}");
	}
}