using VehicleEntity = Bruno.Domain.Entities.Vehicle;

namespace Bruno.Application.DTOs.Vehicle;

public record GetVehicleDto(Guid Id,
							string RegistrationNumber,
							string Make,
							string Model,
							int Year,
							decimal DailyRate,
							DateTime CreatedDate,
							bool IsDeleted)
{
	public static GetVehicleDto FromEntity(VehicleEntity entity) =>
		new(entity.Id, entity.RegistrationNumber, entity.Make, entity.Model,
			entity.Year, entity.DailyRate, entity.CreatedAt, entity.IsDeleted);
}
