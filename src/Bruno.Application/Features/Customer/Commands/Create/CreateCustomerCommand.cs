using MediatR;

namespace Bruno.Application.Features.Customer.Commands.Create;

public record CreateCustomerCommand(string FirstName,
							string LastName,
							string Email,
							string PhoneNumber) : IRequest<Guid>;
