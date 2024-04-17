using listing.core.Domain.Elasticsearch;

namespace listing.core.Abstractions;

public interface IESCategoryRepository
{
	bool Save(Category category);
}
