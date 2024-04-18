using listing.core.Abstractions;
using listing.core.Domain.EFCore;

namespace listing.infrastructure.Data.EFCore.Repositories;

public class EFSaleItemRepository : EFGenericRepository<SaleItem>, IEFSaleItemRepository
{
	public EFSaleItemRepository(EFListingDbContext inventoryDbContext) : base(inventoryDbContext)
	{
	}
}
