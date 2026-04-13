using System.Security.Cryptography;
using System.Text;

namespace Bruno.API.Middleware;

public class ApiKeyMiddleware
{
	private const string ApiKeyHeader = "X-Api-Key";

	private readonly RequestDelegate next;
	private readonly IConfiguration configuration;

	public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
	{
		this.next = next;
		this.configuration = configuration;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		var path = context.Request.Path;

		if (path.StartsWithSegments("/swagger") || path.StartsWithSegments("/openapi"))
		{
			await next(context);
			return;
		}

		if (!context.Request.Headers.TryGetValue(ApiKeyHeader, out var providedKey))
		{
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			await context.Response.WriteAsJsonAsync(new { title = "API key is missing." });
			return;
		}

		var expectedKey = configuration["ApiKey"] ?? string.Empty;

		var expectedBytes = Encoding.UTF8.GetBytes(expectedKey);
		var providedBytes = Encoding.UTF8.GetBytes(providedKey.ToString());

		if (!CryptographicOperations.FixedTimeEquals(providedBytes, expectedBytes))
		{
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			await context.Response.WriteAsJsonAsync(new { title = "Invalid API key." });
			return;
		}

		await next(context);
	}
}
