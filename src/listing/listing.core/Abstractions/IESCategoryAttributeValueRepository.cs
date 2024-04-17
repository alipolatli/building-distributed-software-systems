using listing.core.Domain.Elasticsearch;

namespace listing.core.Abstractions;

public interface IESCategoryAttributeValueRepository
{
	void Bulk(IEnumerable<CategoryAttributeValue> categoryAttribute);
}
