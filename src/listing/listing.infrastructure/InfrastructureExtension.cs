using listing.infrastructure.Clients.Trendyol;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace listing.infrastructure
{
	public static class InfrastructureExtension
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			#region EFCore
			//services.AddDbContext<EFListingDbContext>((serviceProvider, options) =>
			//{
			//	options.UseNpgsql(configuration.GetConnectionString("POSTGRES"), npgsqlOptions =>
			//	{
			//		npgsqlOptions.UseNetTopologySuite();
			//	});
			//	options.AddInterceptors(serviceProvider.GetRequiredService<PublishDomainEventsInterceptor>());
			//});
			//todo add automigrate => dbContext.Database.Migrate();
			#endregion

			services.AddHttpClient<ProductCategoriesHttpClient>(configure=>
			{
				configure.BaseAddress = new Uri(configuration["Trendyol:ProductCategories"]!);
			});
			services.AddHttpClient<AttributesHttpClient>(configure =>
			{
				configure.BaseAddress = new Uri(configuration["Trendyol:Attributes"]!);
			});
			#region Repositories

			#endregion

			return services;
		}
	}
}
