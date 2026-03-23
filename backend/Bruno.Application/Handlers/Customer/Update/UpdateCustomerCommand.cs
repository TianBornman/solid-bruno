using MediatR;

namespace Bruno.Application.Handlers.Customer.Update;

public record UpdateCustomerCommand(Guid Id,
							string FirstName,
							string LastName,
							string Email,
							string PhoneNumber) : IRequest;
