using Bruno.Features.Customer.Models;
using Bruno.Shared.Api;
using Bruno.Shared.Models;

namespace Bruno.Features.Customer.Api;

public class CustomerApi : ICustomerApi
{
	private readonly IApiClient api;

	public CustomerApi(IApiClient api)
	{
		this.api = api;
	}

	public async Task<Guid?> CreateCustomer(CreateCustomerRequest request, CancellationToken ct = default)
	{
		return await api.PostAsync<CreateCustomerRequest, Guid>(Endpoints.Base, request, ct);
	}

	public async Task DeleteCustomer(Guid request, CancellationToken ct = default)
	{
		await api.DeleteAsync($"{Endpoints.Base}/{request}", ct);
	}

	public async Task<CustomerDto?> GetCustomer(Guid id, CancellationToken ct = default)
	{
		return await api.GetAsync<CustomerDto>($"{Endpoints.Base}/{id}", ct);
	}

	public async Task<IReadOnlyList<CustomerDto>> GetCustomers(ListRequest request, CancellationToken ct = default)
	{
		return await api.PostAsync<ListRequest, List<CustomerDto>>(Endpoints.List, request, ct) ?? [];
	}

	public async Task<Guid?> UpdateCustomer(UpdateCustomerRequest request, CancellationToken ct = default)
	{
		return await api.PutAsync<UpdateCustomerRequest, Guid>(Endpoints.Base, request, ct);
	}
}
