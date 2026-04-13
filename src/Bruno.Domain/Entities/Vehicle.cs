using Bruno.Domain.Exceptions;

namespace Bruno.Domain.Entities;

public class Vehicle : BaseEntity
{
	public required string RegistrationNumber { get; set; }
	public required string Make { get; set; }
	public required string Model { get; set; }
	public required int Year { get; set; }
	public required decimal DailyRate { get; set; }
	public bool IsDeleted { get; set; } = false;

	public void Update(string registrationNumber, string make, string model, int year, decimal dailyRate)
	{
		RegistrationNumber = registrationNumber;
		Make = make;
		Model = model;
		Year = year;
		DailyRate = dailyRate;
	}

	public void CanBook()
	{
		if (IsDeleted)
			throw new DomainException("Cannot book a deleted vehicle.");
	}

	public void SoftDelete()
	{
		IsDeleted = true;
	}
}
