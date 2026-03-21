namespace Bruno.Application.DTOs.Vehicle;

public record GetVehicleDto(Guid Id,
							string RegistrationNumber,
							string Make,
							string Model,
							int Year,
							decimal DailyRate,
							DateTime CreatedDate,
							bool IsDeleted);
