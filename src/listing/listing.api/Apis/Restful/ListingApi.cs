using listing.core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace listing.api.Apis.Restful;

public static class ListingApi
{
	public static IEndpointRouteBuilder MapListingApi(this IEndpointRouteBuilder app)
	{
		#region Legacy
		app.MapGet("/category-and-attributes", ProvideCategoryAndAttributes);
		#endregion

		app.MapGet("/top-categories", TopCategories);
		app.MapGet("/categories/{id:int}/parent", GetParentCategories);
		app.MapGet("/categories/{id:int}/subs", GetSubsCategories);
		app.MapGet("/categories/{id:int}/attributes", GetCategoryAttributes);
		app.MapGet("/categories/{categoryId:int}/attributes/{id:int}/values", GetCategoryAttributeValues);

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

	public static async Task<IResult> TopCategories([FromServices] IESCategoryRepository categoryRepository, CancellationToken cancellationToken)
		=> Results.Ok(await categoryRepository.GetTopCategoriesAsync(cancellationToken));

	public static async Task<IResult> GetParentCategories(int id, [FromServices] IESCategoryRepository categoryRepository, CancellationToken cancellationToken)
		=> Results.Ok(await categoryRepository.GetParentCategoryAsync(id, cancellationToken));

	public static async Task<IResult> GetSubsCategories(int id, [FromServices] IESCategoryRepository categoryRepository, CancellationToken cancellationToken)
		=> Results.Ok(await categoryRepository.GetSubCategoriesAsync(id, cancellationToken));

	public static async Task<IResult> GetCategoryAttributes(int id, [FromServices] IESCategoryAttributeRepository categoryAttributeRepository, CancellationToken cancellationToken, string? name = default)
		=> Results.Ok(await categoryAttributeRepository.GetAttributesAsync(id, name, cancellationToken));

	public static async Task<IResult> GetCategoryAttributeValues(int categoryId, int id, [FromServices] IESCategoryAttributeValueRepository categoryAttributeValueRepository, CancellationToken cancellationToken, string? name = default)
		=> Results.Ok(await categoryAttributeValueRepository.GetAttributeValuesAsync(categoryId, id, name, cancellationToken));

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