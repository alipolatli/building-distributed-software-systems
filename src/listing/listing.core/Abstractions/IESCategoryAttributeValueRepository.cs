using listing.core.Domain.Elasticsearch;

namespace listing.core.Abstractions;

public interface IESCategoryAttributeValueRepository
{
	bool Bulk(IEnumerable<CategoryAttributeValue> categoryAttributeValues);
	Task<IEnumerable<CategoryAttributeValue>> GetAttributeValuesAsync(int categoryId, int attributeId, string? name = default , CancellationToken cancellationToken= default);
}
