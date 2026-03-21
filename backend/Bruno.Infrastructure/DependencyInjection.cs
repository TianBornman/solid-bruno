using Bruno.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bruno.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<BrunoContext>(options =>
		{
			options.UseNpgsql(configuration.GetConnectionString("BrunoDb"), b => b.MigrationsAssembly("Bruno.API"));
		});

		return services;
	}
}
