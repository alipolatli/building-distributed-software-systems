using listing.core.Abstractions;
using listing.infrastructure.Clients.Trendyol;
using MediatR;

namespace listing.api.Apis.Restful;

public class ListingServices(CategoryAndAttributeHandler categoryAndAttributeProvider, ISender sender)
{
	public ISender Sender { get; } = sender;
	public CategoryAndAttributeHandler CategoryAndAttributeProvider { get; } = categoryAndAttributeProvider;
}