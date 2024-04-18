using Elastic.Clients.Elasticsearch;
using listing.core.Abstractions;
using listing.infrastructure.Clients.Trendyol;
using listing.infrastructure.Data.EFCore;
using listing.infrastructure.Data.EFCore.Repositories;
using listing.infrastructure.Data.Elasticsearch.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace listing.infrastructure;

public static class InfrastructureExtension
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		#region EF
		services.AddDbContext<EFListingDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("POSTGRES")!));
		#endregion

		#region ES
		services.AddSingleton(sp =>
		{
			return new ElasticsearchClient(new Uri(configuration.GetConnectionString("ELASTICSEARCH")!));
		});
		#endregion

		#region Repositories
		services.AddScoped<IESCategoryRepository, ESCategoryRepository>();
		services.AddScoped<IESCategoryAttributeRepository, ESCategoryAttributeRepository>();
		services.AddScoped<IESCategoryAttributeValueRepository, ESCategoryAttributeValueRepository>();
		services.AddScoped<IEFSaleItemRepository, EFSaleItemRepository>();
		#endregion

		#region HttpClients
		services.AddTransient<CategoryAndAttributeHandler>();

		services.AddHttpClient<ProductCategoriesHttpClient>()
			.ConfigureHttpClient(httpClient =>
			{
				httpClient.BaseAddress = new Uri(configuration["Trendyol:Enpoints:ProductCategories:Path"]!);
			})
			.AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(Convert.ToInt32(configuration["Trendyol:Enpoints:ProductCategories:Retry"]!), retryAttempt => TimeSpan.FromSeconds(Convert.ToInt32(configuration["Trendyol:Enpoints:ProductCategories:WaitSecond"]!))))
			.AddDefaultLogger();

		services.AddHttpClient<AttributesHttpClient>()
			.ConfigureHttpClient(httpClient =>
				{
					httpClient.BaseAddress = new Uri(configuration["Trendyol:Enpoints:Attributes:Path"]!);
				})
			.AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(Convert.ToInt32(configuration["Trendyol:Enpoints:Attributes:Retry"]!), retryAttempt => TimeSpan.FromSeconds(Math.Pow(Convert.ToInt32(configuration["Trendyol:Enpoints:Attributes:WaitSecond"]!), retryAttempt))))
			.AddDefaultLogger();
		#endregion
		return services;
	}
}