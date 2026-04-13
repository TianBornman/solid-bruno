using CustomerEntity = Bruno.Domain.Entities.Customer;

namespace Bruno.Application.DTOs.Customer;

public record GetCustomerDto(Guid Id,
							string FirstName,
							string LastName,
							string Email,
							string PhoneNumber,
							DateTime CreatedDate)
{
	public static GetCustomerDto FromEntity(CustomerEntity entity) =>
		new(entity.Id, entity.FirstName, entity.LastName, entity.Email, entity.PhoneNumber, entity.CreatedAt);
}
