using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace listing.core.Domain.Elasticsearch.SeedWork
{
	public abstract class ESEntity
	{
		[Key]
		[JsonPropertyName("_id")]
		public string Id { get; set; } = null!;
	}
}
