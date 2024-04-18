using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using listing.core.Abstractions;
using listing.core.Domain.Elasticsearch;
using Polly;
using System.Text.Json.Serialization;

namespace listing.infrastructure.Data.Elasticsearch.Repositories;

public class ESCategoryAttributeValueRepository : ESGenericRepository<CategoryAttributeValue>, IESCategoryAttributeValueRepository
{
	[JsonConstructor]
	public ESCategoryAttributeValueRepository(ElasticsearchClient elasticsearchClient) : base(elasticsearchClient)
	{
	}

	public bool Bulk(IEnumerable<CategoryAttributeValue> categoryAttributeValues)
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

                foreach (var entity in categoryAttributeValues)
                {
                    var indexOperation = new BulkIndexOperation<CategoryAttributeValue>(entity);
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

	public async Task<IEnumerable<CategoryAttributeValue>> GetAttributeValuesAsync(int categoryId, int attributeId, string? name = null, CancellationToken cancellationToken = default)
	{
		  SearchResponse<CategoryAttributeValue> response =
            name is null 
            ? await _elasticsearchClient.SearchAsync<CategoryAttributeValue>(s => s
							.Size(10000)
							.Index(INDEX_NAME)
							.Query(q => q
								.Bool(b => b
									.Must(m => m
										.Term(t => t
											.Field(f => f.CategoryId)
											.Value(categoryId)
										),
										m => m.Term(t => t
											.Field(f => f.AttributeId)
											.Value(attributeId)
										)
									)
								)
							)
						)
			: await _elasticsearchClient.SearchAsync<CategoryAttributeValue>(s => s
				.Index(INDEX_NAME)
				.Query(q => q
					.Bool(b => b
						.Must(m => m
							.Term(t => t
								.Field(f => f.CategoryId)
								.Value(categoryId)
							),
							m => m.Term(t => t
								.Field(f => f.AttributeId)
								.Value(attributeId)
							),
							m => m.Bool(b1 => b1
								.Should(
									bs => bs.Wildcard(w => w
										.Field(f => f.Name.Suffix("keyword"))
										.Value($"*{name}*")
									),
									bs => bs.Match(ma => ma
										.Field(f => f.Name)
										.Query(name)
									)
								)
							)
						)
					)
				)
			);
		return response.IsValidResponse ? response.Documents : Enumerable.Empty<CategoryAttributeValue>();
	}
}