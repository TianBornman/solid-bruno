namespace Bruno.Features.Customer.Models;

public record CustomerDto(Guid Id,
							string FirstName,
							string LastName,
							string Email,
							string PhoneNumber,
							DateTime CreatedDate);
