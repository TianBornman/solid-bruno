namespace Bruno.Application.DTOs.Vehicle;

public record CreateVehicleDto(string RegistrationNumber,
							string Make,
							string Model,
							int Year,
							decimal DailyRate);
