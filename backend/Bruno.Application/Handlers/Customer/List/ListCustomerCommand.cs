using Bruno.Application.DTOs.Customer;
using MediatR;

namespace Bruno.Application.Handlers.Customer.List;

public record ListCustomerCommand(int Skip, int Take) : IRequest<List<GetCustomerDto>>;
