using listing.api.Apis.Restful;
using listing.infrastructure;

namespace listing.api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddInfrastructure(builder.Configuration);

			builder.Services.AddAuthorization();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthorization();

			app.MapGroup("/api/v1/listing")
		   .WithTags("Listing API")
		   .MapListingApi();

			app.Run();
		}
	}
}