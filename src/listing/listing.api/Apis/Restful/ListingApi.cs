namespace listing.api.Apis.Restful;

public static class ListingApi
{
	public static IEndpointRouteBuilder MapListingApi(this IEndpointRouteBuilder app)
	{
		app.MapGet("/category-and-attributes", ProvideCategoryAndAttributes);
		return app;
	}

	public static async Task<IResult> ProvideCategoryAndAttributes([AsParameters] ListingServices listingServices, CancellationToken cancellationToken)
	{
		await listingServices.CategoryAndAttributeProvider.HandleAsync(cancellationToken);
		return Results.NoContent();
	}
}
