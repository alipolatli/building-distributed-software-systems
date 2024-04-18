using listing.core.Domain.Elasticsearch;

namespace listing.core.Abstractions;

public interface IESCategoryAttributeRepository
{
	bool Bulk(IEnumerable<CategoryAttribute> categoryAttributes);
}
