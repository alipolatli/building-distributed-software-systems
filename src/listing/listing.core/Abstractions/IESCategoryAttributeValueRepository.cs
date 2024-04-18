using listing.core.Domain.Elasticsearch;

namespace listing.core.Abstractions;

public interface IESCategoryAttributeValueRepository
{
	bool Bulk(IEnumerable<CategoryAttributeValue> categoryAttributeValues);
}
