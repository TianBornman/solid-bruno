using System.Net.Http.Json;
using System.Text.Json;

namespace Bruno.Shared.Api;

public class ApiClient : IApiClient
{
	private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
	private readonly HttpClient _httpClient;

	public ApiClient(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<T?> GetAsync<T>(string uri, CancellationToken ct = default)
	{
		using var response = await _httpClient.GetAsync(uri, ct);
		await EnsureSuccess(response, ct);
		return await response.Content.ReadFromJsonAsync<T>(JsonOptions, ct);
	}

	public async Task<TResponse?> PostAsync<TRequest, TResponse>(
		string uri,
		TRequest body,
		CancellationToken ct = default)
	{
		using var response = await _httpClient.PostAsJsonAsync(uri, body, JsonOptions, ct);
		await EnsureSuccess(response, ct);
		return await response.Content.ReadFromJsonAsync<TResponse>(JsonOptions, ct);
	}

	public async Task<TResponse?> PutAsync<TRequest, TResponse>(
		string uri,
		TRequest body,
		CancellationToken ct = default)
	{
		using var response = await _httpClient.PutAsJsonAsync(uri, body, JsonOptions, ct);
		await EnsureSuccess(response, ct);
		return await response.Content.ReadFromJsonAsync<TResponse>(JsonOptions, ct);
	}

	public async Task DeleteAsync(string uri, CancellationToken ct = default)
	{
		using var response = await _httpClient.DeleteAsync(uri, ct);
		await EnsureSuccess(response, ct);
	}

	private static async Task EnsureSuccess(HttpResponseMessage response, CancellationToken ct)
	{
		if (response.IsSuccessStatusCode)
			return;

		var body = response.Content is null
			? null
			: await response.Content.ReadAsStringAsync(ct);

		throw new ApiException(
			response.StatusCode,
			response.ReasonPhrase,
			body);
	}
}
