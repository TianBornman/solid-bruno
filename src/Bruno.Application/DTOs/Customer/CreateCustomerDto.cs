namespace Bruno.Application.DTOs.Customer;

public record CreateCustomerDto(string FirstName,
							string LastName,
							string Email,
							string PhoneNumber);
