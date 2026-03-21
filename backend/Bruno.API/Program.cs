using Bruno.API.ExceptionHandlers;
using Bruno.Application;
using Bruno.Infrastructure;

namespace Bruno.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Adding modules
			builder.Services.AddApplication();
			builder.Services.AddInfrastructure(builder.Configuration);
			
			builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
			builder.Services.AddProblemDetails();

			builder.Services.AddControllers();

			builder.Services.AddOpenApi();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();

				app.UseSwaggerUI(options =>
				{
					options.SwaggerEndpoint("/openapi/v1.json", "v1");
				});
			}

			app.UseExceptionHandler();
			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
