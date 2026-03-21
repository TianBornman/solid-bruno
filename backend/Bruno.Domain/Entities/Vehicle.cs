namespace Bruno.Domain.Entities;

public class Vehicle : BaseEntity
{
	public required string RegistrationNumber { get; set; }
	public required string Make { get; set; }
	public required string Model { get; set; }
	public required int Year { get; set; }
	public decimal DailyRate { get; set; } = 0;
	public bool IsDeleted { get; set; } = false;
}
