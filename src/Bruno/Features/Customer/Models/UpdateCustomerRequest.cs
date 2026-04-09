namespace Bruno.Features.Customer.Models;

public record UpdateCustomerRequest(Guid Id,
							string FirstName,
							string LastName,
							string Email,
							string PhoneNumber);
