using FluentValidation;
using listing.api.Application.Behaviors;
using listing.core;
using listing.infrastructure;
using System.Reflection;


namespace listing.api.Application;

public static class ApplicationExtension
{
	public static IHostApplicationBuilder AddApplication(this IHostApplicationBuilder builder)
	{
		#region Infrastructure
		builder.Services.AddInfrastructure(builder.Configuration);
		#endregion

		#region Tenancy
		builder.Services.AddHttpContextAccessor();
		builder.Services.AddScoped<TenantProvider>();
		#endregion

		#region MediatR
		builder.Services.AddMediatR(cfg =>
		{
			cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
			cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
			cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
		});
		#endregion

		#region FluentValidation
		ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
		builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		#endregion

		#region gRPC
		builder.Services.AddGrpc();
		#endregion

		return builder;
	}
}
