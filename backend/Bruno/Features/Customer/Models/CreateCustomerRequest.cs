namespace Bruno.Features.Customer.Models;

public record CreateCustomerRequest(string FirstName,
							string LastName,
							string Email,
							string PhoneNumber);
