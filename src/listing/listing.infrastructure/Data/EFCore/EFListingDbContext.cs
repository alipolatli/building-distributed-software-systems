using listing.core.Domain.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace listing.infrastructure.Data.EFCore;

public class EFListingDbContext :DbContext
{
    public DbSet<SaleItem> SaleItems { get; set; }
	public DbSet<SaleItemAttribute> SaleItemAttributes { get; set; }
	public DbSet<SaleItemMedia> SaleItemMedias { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
	}

	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		return base.SaveChangesAsync(cancellationToken);
	}
}