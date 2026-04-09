namespace Bruno.Shared.Api;

public class ApiException : Exception
{
	public System.Net.HttpStatusCode StatusCode { get; }
	public string? ResponseBody { get; }

	public ApiException(System.Net.HttpStatusCode statusCode, string? reasonPhrase, string? responseBody)
		: base($"API request failed: {(int)statusCode} {reasonPhrase}")
	{
		StatusCode = statusCode;
		ResponseBody = responseBody;
	}
}
