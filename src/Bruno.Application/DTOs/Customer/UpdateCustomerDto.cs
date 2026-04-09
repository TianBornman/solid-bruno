namespace Bruno.Application.DTOs.Customer;

public record UpdateCustomerDto(Guid Id,
							string FirstName,
							string LastName,
							string Email,
							string PhoneNumber);
