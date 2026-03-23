namespace Bruno.Application.DTOs.Customer;

public record GetCustomerDto(Guid Id,
							string FirstName,
							string LastName,
							string Email,
							string PhoneNumber,
							DateTime CreatedDate);
