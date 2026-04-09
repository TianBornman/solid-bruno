namespace Bruno.Shared.Api;

public interface IApiClient
{
	Task<T?> GetAsync<T>(string uri, CancellationToken ct = default);
	Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest body, CancellationToken ct = default);
	Task<TResponse?> PutAsync<TRequest, TResponse>(string uri, TRequest body, CancellationToken ct = default);
	Task DeleteAsync(string uri, CancellationToken ct = default);
}
