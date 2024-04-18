using Elastic.Clients.Elasticsearch;
using listing.core.Abstractions;
using listing.core.Domain.Elasticsearch;
using Polly;

namespace listing.infrastructure.Data.Elasticsearch;

public class ESCategoryRepository(ElasticsearchClient _elasticsearchClient) : IESCategoryRepository
{
	private const string INDEX_NAME = "categories";

	public bool Save(Category category)
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

}
