using Bruno.Application.DTOs.Customer;
using MediatR;

namespace Bruno.Application.Features.Customer.Queries.List;

public record ListCustomerQuery(int Skip, int Take, string? Search) : IRequest<List<GetCustomerDto>>;
