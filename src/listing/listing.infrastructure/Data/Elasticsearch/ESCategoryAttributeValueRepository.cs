using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using listing.core.Abstractions;
using listing.core.Domain.Elasticsearch;
using Polly;

namespace listing.infrastructure.Data.Elasticsearch;

public class ESCategoryAttributeValueRepository(ElasticsearchClient _elasticsearchClient) : IESCategoryAttributeValueRepository
{
	private const string INDEX_NAME = "category-attribute-values";

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
}
