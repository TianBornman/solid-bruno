using Bruno.Features.Customer.Models;
using Bruno.Shared.Api;

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

    public async Task DeleteCustomer(Guid id, CancellationToken ct = default)
    {
        await api.DeleteAsync($"{Endpoints.Base}/{id}", ct);
    }

    public async Task<CustomerDto?> GetCustomer(Guid id, CancellationToken ct = default)
    {
        return await api.GetAsync<CustomerDto>($"{Endpoints.Base}/{id}", ct);
    }

    public async Task<IReadOnlyList<CustomerDto>> GetCustomers(ListCustomerRequest request, CancellationToken ct = default)
    {
        var query = $"?skip={request.Skip}&take={request.Take}";
        if (!string.IsNullOrWhiteSpace(request.Search))
            query += $"&search={Uri.EscapeDataString(request.Search)}";

        return await api.GetAsync<List<CustomerDto>>($"{Endpoints.Base}{query}", ct) ?? [];
    }

    public async Task<Guid?> UpdateCustomer(UpdateCustomerRequest request, CancellationToken ct = default)
    {
        return await api.PutAsync<UpdateCustomerRequest, Guid>(Endpoints.Base, request, ct);
    }
}
