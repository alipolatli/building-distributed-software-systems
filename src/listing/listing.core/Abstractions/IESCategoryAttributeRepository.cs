using listing.core.Domain.Elasticsearch;

namespace listing.core.Abstractions;

public interface IESCategoryAttributeRepository : IESGenericRepository<CategoryAttribute>
{
	bool Bulk(IEnumerable<CategoryAttribute> categoryAttributes);
	Task<IEnumerable<CategoryAttribute>> GetAttributesAsync(int categoryId, string? name = default, CancellationToken cancellationToken = default);
}
