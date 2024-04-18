using listing.core.Abstractions;
using listing.core.Domain.EFCore.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace listing.infrastructure.Data.EFCore.Repositories;

public class EFGenericRepository<T> : IEFGenericRepository<T> where T : EFEntity, IAggregateRoot
{
	protected readonly EFListingDbContext _dbContext;
	public IUnitOfWork UnitOfWork => _dbContext;

	public EFGenericRepository(EFListingDbContext inventoryDbContext)
	{
		_dbContext = inventoryDbContext ?? throw new ArgumentNullException(nameof(inventoryDbContext));
	}

	public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
		 => await _dbContext.Set<T>().AddAsync(entity, cancellationToken);

	public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
		 => await _dbContext.Set<T>().AddRangeAsync(entities, cancellationToken);

	public async Task<bool> AnyAsync(Guid id, CancellationToken cancellationToken = default)
		=> await _dbContext.Set<T>().AnyAsync(e => e.Id == id, cancellationToken);

	public async Task<bool> AnyAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default)
		=> await _dbContext.Set<T>().AnyAsync(filter ?? (_ => true), cancellationToken);

	public IQueryable<T> AsQueryable(Expression<Func<T, bool>>? filter = null)
		=> _dbContext.Set<T>().Where(filter ?? (_ => true));

	public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
		=> await _dbContext.Set<T>().ToListAsync(cancellationToken);

	public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includes)
	{
		var query = _dbContext.Set<T>().Where(filter ?? (_ => true));
		foreach (var include in includes)
		{
			query = query.Include(include);
		}

		return await query.ToListAsync();
	}

	public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes)
	{
		var query = _dbContext.Set<T>().Where(filter ?? (_ => true));
		foreach (var include in includes)
		{
			query = query.Include(include);
		}
		return await (orderBy?.Invoke(query) ?? query).ToListAsync();
	}


	public async Task<PaginatedResult<T>> GetAllPaginatedAsync(int page, int size, CancellationToken cancellationToken = default)
	{
		var total = await _dbContext.Set<T>().LongCountAsync(cancellationToken);
		var data = await _dbContext.Set<T>()
							 .Skip((page - 1) * size)
							 .Take(size)
							 .ToListAsync(cancellationToken);

		return new PaginatedResult<T>(data, total);
	}

	public async Task<PaginatedResult<T>> GetAllPaginatedAsync(int page, int size, Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includes)
	{
		var query = _dbContext.Set<T>().Where(filter ?? (_ => true));
		_ = includes.Aggregate(query, (current, include) => current.Include(include));

		long total = await query.LongCountAsync();
		var data = await query
							 .Skip((page - 1) * size)
							 .Take(size)
							 .ToListAsync();

		return new PaginatedResult<T>(data, total);
	}

	public async Task<PaginatedResult<T>> GetAllPaginatedOrderingAsync(int page, int size, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes)
	{
		var query = _dbContext.Set<T>().Where(filter ?? (_ => true));
		foreach (var include in includes)
		{
			query = query.Include(include);
		}
		long total = await query.LongCountAsync();
		query = orderBy?.Invoke(query) ?? query;
		var data = await query
						  .Skip((page - 1) * size)
						  .Take(size)
						  .ToListAsync();
		return new PaginatedResult<T>(data, total);
	}

	public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includes)
	{
		var query = _dbContext.Set<T>().Where(filter ?? (_ => true));
		foreach (var include in includes)
		{
			query = query.Include(include);
		}
		return await query.SingleOrDefaultAsync();
	}

	public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
		=> await _dbContext.Set<T>().FindAsync(id, cancellationToken);

	public void Remove(T entity)
		=> _dbContext.Set<T>().Remove(entity);

	public void RemoveRange(IEnumerable<T> entities)
		=> _dbContext.Set<T>().RemoveRange(entities);

	public Task<long> TotalCountAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default)
		=> _dbContext.Set<T>().LongCountAsync(filter ?? (_ => true), cancellationToken);

	public void Update(T entity)
		=> _dbContext.Set<T>().Update(entity);

	public void UpdateRange(IEnumerable<T> entities)
		=> _dbContext.Set<T>().UpdateRange(entities);
}