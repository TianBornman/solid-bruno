using MediatR;

namespace Bruno.Application.Features.Customer.Commands.Update;

public record UpdateCustomerCommand(Guid Id,
							string FirstName,
							string LastName,
							string Email,
							string PhoneNumber) : IRequest;
