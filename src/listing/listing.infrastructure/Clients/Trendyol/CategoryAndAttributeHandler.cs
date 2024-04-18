using listing.core.Abstractions;
using listing.core.Domain.Elasticsearch;
using listing.core.Extensions;

namespace listing.infrastructure.Clients.Trendyol;

public class CategoryAndAttributeHandler(IESCategoryRepository _eSCategoryRepository, IESCategoryAttributeRepository _eSCategoryAttributeRepository, IESCategoryAttributeValueRepository _eSCategoryAttributeValueRepository, ProductCategoriesHttpClient _productCategoriesHttpClient, AttributesHttpClient _attributesHttpClient)
{
	private const string _hierarchyName = "hierarchy-name";
	private const string _hierarchyId = "hierarchy-id";

	public async Task HandleAsync(CancellationToken cancellationToken = default)
	{
		IEnumerable<TrendyolCategory> productCategories= (await _productCategoriesHttpClient.GetProductCategoriesAsync(cancellationToken)).categories;
		IEnumerable<TrendyolCategory> flatProductCategories = productCategories.SelectRecursive(c => c.subCategories);
		await HandleAndSaveAsync(productCategories, flatProductCategories);
	}

	private async Task HandleAndSaveAsync(IEnumerable<TrendyolCategory> productCategories, IEnumerable<TrendyolCategory> flatProductCategories)
	{
		foreach (var productCategory in productCategories)
		{
			SaveCategory(productCategory, flatProductCategories);
			if (productCategory.subCategories.Any())
				await HandleAndSaveAsync(productCategory.subCategories, flatProductCategories);
			else
			{
				TrendyolAttributeRootObject attributes = await _attributesHttpClient.GetAttributesAsync(productCategory.id);
				if (attributes.categoryAttributes.Any())
				{
					SaveAttributes(attributes.categoryAttributes);
				}
			}
		}
	}

	private void SaveCategory(TrendyolCategory productCategory, IEnumerable<TrendyolCategory> flatProductCategories)
	{
		var hieararchyPaths = GenerateHierarchy(productCategory, flatProductCategories);
		var category = new Category(productCategory.id, productCategory.name, productCategory.parentId, hieararchyPaths.GetValueOrDefault(_hierarchyName, string.Empty), hieararchyPaths.GetValueOrDefault(_hierarchyId, string.Empty), !productCategory.subCategories.Any());
		 _eSCategoryRepository.Add(category);
	}

	private void SaveAttributes(IEnumerable<TrendyolAttribute> attributes)
	{
		IEnumerable<CategoryAttribute> categoryAttributes = attributes.Select(ta => new CategoryAttribute(ta.categoryId, ta.attribute.id, ta.attribute.name, ta.required, ta.varianter, ta.slicer, ta.allowCustom));
		_eSCategoryAttributeRepository.Bulk(categoryAttributes);

		foreach (var attribute in attributes)
		{
			if (attribute.attributeValues.Any())
				SaveAttributeValue(attribute.categoryId, attribute.attribute.id, attribute.attributeValues);
		}
	}

	private void SaveAttributeValue(int categoryId, int attributeId, IEnumerable<TrendyolAttributeValue> attributeValues)
	{
		IEnumerable<CategoryAttributeValue> categoryAttributeValues = attributeValues.Select(tav => new CategoryAttributeValue(categoryId, attributeId, tav.id, tav.name));
		_eSCategoryAttributeValueRepository.Bulk(categoryAttributeValues);
	}

	private Dictionary<string, string> GenerateHierarchy(TrendyolCategory category, IEnumerable<TrendyolCategory> flatProductCategories)
	{
		var hierarchyIds = new List<int>();
		var hierarchyNames = new List<string>();

		if (!category.parentId.HasValue)
		{
			return new Dictionary<string, string>
				{
					{ _hierarchyId, category.id.ToString() },
					{ _hierarchyName, category.name },
				};
		}

		while (category.parentId.HasValue)
		{
			hierarchyIds.Add(category.id);
			hierarchyNames.Add(category.name);
			category = flatProductCategories.First(flatCategory => flatCategory.id == category.parentId);

			if (!category.parentId.HasValue && category.subCategories.Any())
			{
				hierarchyIds.Add(category.id);
				hierarchyNames.Add(category.name);
			}
		}
		hierarchyIds.Reverse();
		hierarchyNames.Reverse();
		var hierarchyNamePath = string.Join(" » ", hierarchyNames);
		var hierarchyIdPath = string.Join(" » ", hierarchyIds);
		return new Dictionary<string, string>
		{
			{ _hierarchyId, hierarchyIdPath },
			{ _hierarchyName, hierarchyNamePath },
		};
	}
}

