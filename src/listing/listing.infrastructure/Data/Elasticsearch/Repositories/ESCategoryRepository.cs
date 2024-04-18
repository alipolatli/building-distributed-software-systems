using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using listing.core.Abstractions;
using listing.core.Domain.Elasticsearch;
using Polly;
using System.Text.Json.Serialization;

namespace listing.infrastructure.Data.Elasticsearch.Repositories;

public class ESCategoryRepository : ESGenericRepository<Category>, IESCategoryRepository
{
	[JsonConstructor]
	public ESCategoryRepository(ElasticsearchClient elasticsearchClient) : base(elasticsearchClient)
	{

	}

	public bool Add(Category category)
	{
		var policy = Policy
						.Handle<Exception>()
						.Or<TimeoutException>()
						.WaitAndRetry(retryCount: 3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
		try
		{
			return policy.Execute(() =>
			{
				var indexResponse = _elasticsearchClient.Index(category, INDEX_NAME);
				return indexResponse.IsSuccess();
			});
		}
		catch (Exception ex)
		{
			throw new Exception($"Retry mechanism still not successful at the end. Exception Message: {ex.Message} ", ex);
		}
	}

	public async Task<IEnumerable<Category>> GetTopCategoriesAsync(CancellationToken cancellationToken = default)
	{
		SearchResponse<Category> response = await _elasticsearchClient.SearchAsync<Category>(s => s
						.Index(INDEX_NAME)
						.Size(10000)
						.Query(q => q
							.Bool(b => b
								.MustNot(mn => mn
									.Exists(e => e.Field(f => f.ParentId))
								)
							)
						), cancellationToken
					);
		return response.IsValidResponse ? response.Documents : Enumerable.Empty<Category>();
	}

	public async Task<Category?> GetParentCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
	{
		SearchResponse<Category>? thisCategoryResponse = await _elasticsearchClient.SearchAsync<Category>(s => s.Index(INDEX_NAME)
						.Query(q => q
							.Term(t => t
								.Field(f => f.CategoryId)
								.Value(categoryId)
							)
						), cancellationToken
					);

		if (thisCategoryResponse.Total < 1)
			return null;

		int? parentId = thisCategoryResponse.Documents.FirstOrDefault()?.ParentId;
		if (!parentId.HasValue)
			return null;

		SearchResponse<Category> parentCategoryResponse = await _elasticsearchClient.SearchAsync<Category>(s => s.Index(INDEX_NAME)
				.Query(q => q
					.Term(t => t
						.Field(f => f.CategoryId)
						.Value(parentId.Value)
						)
					), cancellationToken
			);
		return parentCategoryResponse.Total >= 1 ? parentCategoryResponse.Documents.FirstOrDefault() : null;
	}

	public async Task<IEnumerable<Category>> GetSubCategoriesAsync(int categoryId, CancellationToken cancellationToken = default)
	{
		SearchResponse<Category> response = await _elasticsearchClient.SearchAsync<Category>(s => s.Index(INDEX_NAME)
						.Size(10000) //becasuse I want all of them
						.Query(q => q
							.Term(t => t
								.Field(f => f.ParentId)
								.Value(categoryId)
							)
						), cancellationToken
					);

		return response.IsValidResponse ? response.Documents : Enumerable.Empty<Category>();
	}

	public async Task<IEnumerable<Category>> SearchAsync(string categoryName, bool onlyAvailable = false, CancellationToken cancellationToken = default)
	{
		SearchResponse<Category> response =
			onlyAvailable is true
				? await _elasticsearchClient.SearchAsync<Category>(s => s
						.Index(INDEX_NAME)
						.Size(100)
						.Query(q => q
							.Bool(b => b
								.Should(sh => sh
									.Match(m => m
										.Field(f => f.HierarchyName)
										.Query(categoryName)
										.Operator(Operator.Or)
										.Fuzziness(new Fuzziness("auto"))
										.PrefixLength(1)
										.MaxExpansions(10)
									),
									sh => sh
									.MatchPhrasePrefix(mp => mp
										.Field(f => f.HierarchyName)
										.Query(categoryName)
										.MaxExpansions(10)
									),
									sh => sh
									.Wildcard(w => w
										.Field(f => f.HierarchyName.Suffix("keyword"))
										.Value($"*{categoryName}*")
									)
								)
								.Must(m => m
									.Term(t => t
										.Field(f => f.Available)
										.Value(true)
									)
								)
							)
						), cancellationToken
					)
				: await _elasticsearchClient.SearchAsync<Category>(s => s
					.Index(INDEX_NAME)
					.Size(100)
					.Query(q => q
						.Bool(b => b
							.Should(sh => sh
								.Match(m => m
									.Field(f => f.HierarchyName)
									.Query(categoryName)
									.Operator(Operator.Or)
									.Fuzziness(new Fuzziness("auto"))
									.PrefixLength(1)
									.MaxExpansions(10)
								),
								sh => sh
								.MatchPhrasePrefix(mp => mp
									.Field(f => f.HierarchyName)
									.Query(categoryName)
									.MaxExpansions(10)
								),
								sh => sh
								.Wildcard(w => w
									.Field(f => f.HierarchyName.Suffix("keyword"))
									.Value($"*{categoryName}*")
								)
							)
						)
					), cancellationToken
				);

		return response.IsValidResponse ? response.Documents : Enumerable.Empty<Category>();
	}
}