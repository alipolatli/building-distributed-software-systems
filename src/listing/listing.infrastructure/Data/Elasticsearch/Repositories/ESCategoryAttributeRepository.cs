using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using Elastic.Clients.Elasticsearch.QueryDsl;
using listing.core.Abstractions;
using listing.core.Domain.Elasticsearch;
using Polly;
using System.Text.Json.Serialization;

namespace listing.infrastructure.Data.Elasticsearch.Repositories;

public class ESCategoryAttributeRepository : ESGenericRepository<CategoryAttribute>, IESCategoryAttributeRepository
{
	[JsonConstructor]
	public ESCategoryAttributeRepository(ElasticsearchClient elasticsearchClient) : base(elasticsearchClient)
	{

	}

	public bool Bulk(IEnumerable<CategoryAttribute> categoryAttributes)
	{
		var policy = Policy
				.Handle<Exception>()
				.Or<TimeoutException>()
				.WaitAndRetry(retryCount: 3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
		try
		{
			return policy.Execute(() =>
			{
				var bulkRequest = new BulkRequest(INDEX_NAME)
				{
					Operations = new List<IBulkOperation>()
				};

				foreach (var entity in categoryAttributes)
				{
					var indexOperation = new BulkIndexOperation<CategoryAttribute>(entity);
					bulkRequest.Operations.Add(indexOperation);
				}

				var bulkResponse = _elasticsearchClient.Bulk(bulkRequest);
				return bulkResponse.IsValidResponse;
			});
		}
		catch (Exception ex)
		{
			throw new Exception($"Retry mechanism still not successful at the end. Exception Message: {ex.Message}", ex);
		}
	}

	public async Task<IEnumerable<CategoryAttribute>> GetAttributesAsync(int categoryId, string? name = null, CancellationToken cancellationToken = default)
	{
		SearchResponse<CategoryAttribute> response =
			name is not null
			? await _elasticsearchClient.SearchAsync<CategoryAttribute>(s => s
				.Index(INDEX_NAME)
				.Query(q => q
					.Bool(b => b
						.Must(m => m
							.Term(t => t
								.Field(f => f.CategoryId)
								.Value(categoryId)
							),
							m => m
							.Bool(mb => mb
								.Should(sh => sh
									.Match(ma => ma
										.Field(f => f.Name)
										.Query(name)
										.Operator(Operator.Or)
										.Fuzziness(new Fuzziness("auto"))
										.PrefixLength(1)
										.MaxExpansions(10)
									),
									sh => sh
									.MatchPhrasePrefix(mp => mp
										.Field(f => f.Name)
										.Query(name)
										.MaxExpansions(10)
									),
									sh => sh
									.Wildcard(w => w
										.Field(f => f.Name)
										.Value($"*{name}*")
									)
								)
							)
						)
					)
				),cancellationToken
			)
			: await _elasticsearchClient.SearchAsync<CategoryAttribute>(s => s
						.Index(INDEX_NAME)
						.Size(1000)
						.Query(q => q
							.Bool(b => b
								.Must(m => m
									.Term(t => t
										.Field(f => f.CategoryId)
										.Value(categoryId)
									)
								)
							)
						)
						.Sort(so => so
							.Field(f => f.Required)
							.Score(sc => sc.Order(SortOrder.Desc)
						)),cancellationToken
					);

		return response.IsValidResponse ? response.Documents : Enumerable.Empty<CategoryAttribute>();
	}
}