using Bruno.Features.Customer.Api;
using Bruno.Features.Customer.Models;
using Bruno.Shared.Models;

namespace Bruno.Features.Customer.State;

public class CustomerState
{
	public IReadOnlyList<CustomerDto> Customers { get; private set; } = [];
	public bool IsLoading { get; private set; }

	private readonly ICustomerApi api;

	public CustomerState(ICustomerApi api)
	{
		this.api = api;
	}

	public async Task Load(ListRequest request)
	{
		IsLoading = true;
		Customers = await api.GetCustomers(request);
		IsLoading = false;
	}
}
