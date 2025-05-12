using Library.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Serilog;

namespace Library.Dependency_Injection
{
	public static class SharedServiceContainer
	{
		public static IServiceCollection AddSharedServices<TContext>(this IServiceCollection services, IConfiguration config, string fileName) where TContext:DbContext
		{
			// Add generic database context

			services.AddDbContext<TContext>(options => options.UseSqlServer(config.GetConnectionString("eCommerceConnection"), sqlServerOption =>
			sqlServerOption.EnableRetryOnFailure()));

			// Config Serilog Logging
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Information()
				.WriteTo.Debug()
				.WriteTo.Console()
				.WriteTo.File(path: $"{fileName}-.text",
				restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
				outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {message:lj}{NewLine}{Exception}",
				rollingInterval: RollingInterval.Day)
				.CreateLogger();

			// Add JWT authentication Scheme
			JWTAuthenticationScheme.AddJWTAuthenticationScheme(services, config);

			return services;	
		}

		public static IApplicationBuilder useSharedPolicies(this IApplicationBuilder app)
		{
			// use global exception
			app.UseMiddleware<GlobalException>();

			// use listen to only api gateway
			app.UseMiddleware<ListenToOnlyApiGateway>();

			return app;
		}
	}
}
