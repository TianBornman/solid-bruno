namespace Bruno.Domain.Entities;

public class Customer : BaseEntity
{
	public required string FirstName { get; set; }
	public required string LastName { get; set; }
	public required string Email { get; set; }
	public required string PhoneNumber { get; set; }

	public void Update(string firstName, string lastName, string email, string phoneNumber)
	{
		FirstName = firstName;
		LastName = lastName;
		Email = email;
		PhoneNumber = phoneNumber;
	}
}
