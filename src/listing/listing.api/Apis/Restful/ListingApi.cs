namespace listing.api.Apis.Restful;

public static class ListingApi
{
	public static IEndpointRouteBuilder MapListingApi(this IEndpointRouteBuilder app)
	{
		#region Legacy
		app.MapGet("/category-and-attributes", ProvideCategoryAndAttributes);
		#endregion
		app.MapGet("/categories", GetCategories);
		app.MapGet("/categories/{int:id}/parent", GetParentCategories);
		app.MapGet("/categories/{int:id}/subs", GetSubsCategories);

		app.MapGet("/categories/{int:id}/attributes", GetCategoryAttributes);
		app.MapGet("/categories/{categoryId:id}/attributes/{int:id}/values", GetCategoryAttributeValues);

		app.MapPost("/sale-items", AddSaleItem);
		app.MapPut("/sale-items/{id:int}", UpdateSaleItem);
		app.MapDelete("sale-items/{id:int}", DeleteSaleItem);

		app.MapPost("/sale-items/{saleItem:int}/stock-items/{id:int}", AddStockItemMedias);
		app.MapPatch("/sale-items/{saleItem:int}/stock-items/{id:int}", UpdateStockAndPrice);
		app.MapDelete("/sale-items/{saleItem:int}/stock-items/{id:int}", DeleteStockItem);
		return app;
	}

	#region Legacy
	public static async Task<IResult> ProvideCategoryAndAttributes([AsParameters] ListingServices listingServices, CancellationToken cancellationToken)
	{
		await listingServices.CategoryAndAttributeProvider.HandleAsync(cancellationToken);
		return Results.NoContent();
	}
	#endregion

	public static async Task<IResult> GetCategories([AsParameters] ListingServices listingServices, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
		// Implementation
	}

	public static async Task<IResult> GetParentCategories([AsParameters] ListingServices listingServices, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
		// Implementation
	}

	public static async Task<IResult> GetSubsCategories([AsParameters] ListingServices listingServices, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
		// Implementation
	}

	public static async Task<IResult> GetCategoryAttributes([AsParameters] ListingServices listingServices, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
		// Implementation
	}

	public static async Task<IResult> GetCategoryAttributeValues([AsParameters] ListingServices listingServices, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
		// Implementation
	}

	public static async Task<IResult> AddSaleItem([AsParameters] ListingServices listingServices, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
		// Implementation
	}

	public static async Task<IResult> UpdateSaleItem([AsParameters] ListingServices listingServices, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();

		// Implementation
	}

	public static async Task<IResult> DeleteSaleItem([AsParameters] ListingServices listingServices, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
		// Implementation
	}

	public static async Task<IResult> AddStockItemMedias([AsParameters] ListingServices listingServices, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
		// Implementation
	}

	public static async Task<IResult> UpdateStockAndPrice([AsParameters] ListingServices listingServices, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
		// Implementation
	}

	public static async Task<IResult> DeleteStockItem([AsParameters] ListingServices listingServices, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
		// Implementation
	}
}