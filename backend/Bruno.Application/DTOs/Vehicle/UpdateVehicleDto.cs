namespace Bruno.Application.DTOs.Vehicle;

public record UpdateVehicleDto(Guid Id,
							string RegistrationNumber,
							string Make,
							string Model,
							int Year,
							decimal DailyRate);
