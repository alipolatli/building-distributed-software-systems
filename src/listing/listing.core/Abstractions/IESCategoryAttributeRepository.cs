using listing.core.Domain.Elasticsearch;

namespace listing.core.Abstractions;

public interface IESCategoryAttributeRepository
{
	void Bulk(IEnumerable<CategoryAttribute> categoryAttribute);
}
