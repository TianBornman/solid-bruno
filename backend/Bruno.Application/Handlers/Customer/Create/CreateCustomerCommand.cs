using MediatR;

namespace Bruno.Application.Handlers.Customer.Create;

public record CreateCustomerCommand(string FirstName,
							string LastName,
							string Email,
							string PhoneNumber) : IRequest<Guid>;
