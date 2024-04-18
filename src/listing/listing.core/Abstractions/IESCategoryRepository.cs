using listing.core.Domain.Elasticsearch;

namespace listing.core.Abstractions;

public interface IESCategoryRepository : IESGenericRepository<Category>
{
	bool Add(Category category);
	Task<IEnumerable<Category>> GetTopCategoriesAsync(CancellationToken cancellationToken = default);
	Task<IEnumerable<Category>> GetSubCategoriesAsync(int categoryId, CancellationToken cancellationToken = default);
	Task<Category?> GetParentCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
	Task<IEnumerable<Category>> SearchAsync(string categoryName, bool onlyAvailable= default, CancellationToken cancellationToken = default);
}
