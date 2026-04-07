using Bruno.API.ExceptionHandlers;
using Bruno.API.Middleware;
using Bruno.Application;
using Bruno.Infrastructure;
using Microsoft.OpenApi;

namespace Bruno.API;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Adding modules
		builder.Services.AddApplication();
		builder.Services.AddInfrastructure(builder.Configuration);

		builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
		builder.Services.AddExceptionHandler<DomainExceptionHandler>();
		builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
		builder.Services.AddProblemDetails();

		builder.Services.AddCors(options =>
		{
			options.AddPolicy("BlazorClient", policy =>
				policy.WithOrigins(
					"https://localhost:7037",
					"http://localhost:5188")
				.AllowAnyHeader()
				.AllowAnyMethod());
		});

		builder.Services.AddControllers();

		builder.Services.AddSwaggerGen(options =>
		{
			options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
			{
				Type = SecuritySchemeType.ApiKey,
				In = ParameterLocation.Header,
				Name = "X-Api-Key",
				Description = "Enter your API key in the field below."
			});

			options.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
			{
				{ new OpenApiSecuritySchemeReference("ApiKey", doc), [] }
			});
		});

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();

			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
			});
		}

		app.UseExceptionHandler();
		app.UseHttpsRedirection();
		app.UseCors("BlazorClient");
		app.UseMiddleware<ApiKeyMiddleware>();

		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}
