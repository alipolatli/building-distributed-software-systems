using listing.infrastructure.Clients.Trendyol;

namespace listing.api.Apis.Restful;

public class ListingServices(CategoryAndAttributeHandler categoryAndAttributeProvider)
{
    public CategoryAndAttributeHandler CategoryAndAttributeProvider { get; } = categoryAndAttributeProvider;
}
