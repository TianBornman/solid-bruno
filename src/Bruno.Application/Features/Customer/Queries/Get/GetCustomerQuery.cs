using Bruno.Application.DTOs.Customer;
using MediatR;

namespace Bruno.Application.Features.Customer.Queries.Get;

public record GetCustomerQuery(Guid Id) : IRequest<GetCustomerDto?>;
