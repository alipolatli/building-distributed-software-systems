using MediatR;

namespace listing.core.Domain.EFCore.SeedWork;

public abstract class EFEntity
{
	int? _requestedHashCode;
	int _Id;
	public virtual int Id
	{
		get
		{
			return _Id;
		}
		protected set
		{
			_Id = value;
		}
	}

	private List<INotification> _domainEvents;
	public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

	public void AddDomainEvent(INotification eventItem)
	{
		_domainEvents = _domainEvents ?? new List<INotification>();
		_domainEvents.Add(eventItem);
	}

	public void RemoveDomainEvent(INotification eventItem)
	{
		_domainEvents?.Remove(eventItem);
	}

	public void ClearDomainEvents()
	{
		_domainEvents?.Clear();
	}

	public bool IsTransient()
	{
		return Id == default;
	}

	public override bool Equals(object obj)
	{
		if (obj == null || !(obj is EFEntity))
			return false;

		if (ReferenceEquals(this, obj))
			return true;

		if (GetType() != obj.GetType())
			return false;

		EFEntity item = (EFEntity)obj;

		if (item.IsTransient() || IsTransient())
			return false;
		else
			return item.Id == Id;
	}

	public override int GetHashCode()
	{
		if (!IsTransient())
		{
			if (!_requestedHashCode.HasValue)
				_requestedHashCode = Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

			return _requestedHashCode.Value;
		}
		else
			return base.GetHashCode();

	}
	public static bool operator ==(EFEntity left, EFEntity right)
	{
		if (Equals(left, null))
			return Equals(right, null) ? true : false;
		else
			return left.Equals(right);
	}

	public static bool operator !=(EFEntity left, EFEntity right)
	{
		return !(left == right);
	}
}