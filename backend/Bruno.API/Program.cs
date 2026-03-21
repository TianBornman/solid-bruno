using Bruno.Infrastructure;

namespace Bruno.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Adding modules
			builder.Services.AddInfrastructure(builder.Configuration);

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

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
