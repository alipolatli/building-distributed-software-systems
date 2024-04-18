using listing.core.Domain.Elasticsearch.SeedWork;
using System.Linq.Expressions;

namespace listing.core.Abstractions;

public interface IESGenericRepository<T> where T : ESEntity
{
	Task<string?> AddAsync(T entity);
	Task<bool> BulkAsync(IEnumerable<T> entities);
	Task<bool> UpdateAsync(T entity);
	Task<bool> DeleteAsync(string id);
	Task<T?> GetByIdAsync(string id);
	Task<T?> GetByIdsAsync(params (Expression<Func<T, string>> fieldSelector, string value)[] fieldSelectors);

	Task<bool> IsExistsByIdAsync(Expression<Func<T, string>> fieldSelector, string value);
	Task<bool> IsExistsByIdsAsync(params (Expression<Func<T, string>> fieldSelector, string value)[] fieldSelectors);
}
