using Bruno.Application.Helpers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Bruno.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

		services.AddMediatR(options =>
		{
			options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
			options.AddOpenBehavior(typeof(ValidationBehavior<,>));
		});

		return services;
	}
}
