using listing.core.Domain.Elasticsearch;

namespace listing.core.Abstractions;

public interface IESCategoryAttributeRepository : IESGenericRepository<CategoryAttribute>
{
	bool Bulk(IEnumerable<CategoryAttribute> categoryAttributes);
}
