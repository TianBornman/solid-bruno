using Bruno.Application.DTOs.Customer;
using MediatR;

namespace Bruno.Application.Handlers.Customer.Get;

public record GetCustomerCommand(Guid Id) : IRequest<GetCustomerDto?>;
