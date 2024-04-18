namespace listing.core.Domain.EFCore.SeedWork;

public interface IRepository<T> where T : class
{
    IUnitOfWork UnitOfWork { get; }
}
