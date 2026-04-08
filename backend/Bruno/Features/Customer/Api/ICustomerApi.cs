using Bruno.Features.Customer.Models;

namespace Bruno.Features.Customer.Api;

public interface ICustomerApi
{
    Task<Guid?> CreateCustomer(CreateCustomerRequest request, CancellationToken ct = default);
    Task DeleteCustomer(Guid id, CancellationToken ct = default);
    Task<CustomerDto?> GetCustomer(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<CustomerDto>> GetCustomers(ListCustomerRequest request, CancellationToken ct = default);
    Task<Guid?> UpdateCustomer(UpdateCustomerRequest request, CancellationToken ct = default);
}
