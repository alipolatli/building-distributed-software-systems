using listing.core;
using listing.core.Domain.EFCore;
using listing.core.Domain.EFCore.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace listing.infrastructure.Data.EFCore;

public class EFListingDbContext : DbContext, IUnitOfWork
{
	private readonly TenantProvider _tenantProvider;
	private readonly Guid TENANT_ID;

	public EFListingDbContext(TenantProvider tenantProvider)
	{
		_tenantProvider = tenantProvider ?? throw new ArgumentNullException(nameof(TenantProvider));
		TENANT_ID = tenantProvider.GetTenantId();
	}

	public DbSet<SaleItem> SaleItems { get; set; }
	public DbSet<StockItem> StockItems { get; set; }
	public DbSet<StockItemMedia> StockItemMedias { get; set; }
	public DbSet<StockItemAttribute> StockItemAttributes { get; set; }


	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=bdss.listing.v1;Username=postgres;Password=example");
		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly())
			.AddTenancyProperties()
			.AddTenancyQueries(TENANT_ID);
	}

	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		return base.SaveChangesAsync(cancellationToken);
	}

	public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}