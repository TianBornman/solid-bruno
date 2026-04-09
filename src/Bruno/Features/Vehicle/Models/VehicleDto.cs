namespace Bruno.Features.Vehicle.Models;

public record VehicleDto(Guid Id,
                    string RegistrationNumber,
                    string Make,
                    string Model,
                    int Year,
                    decimal DailyRate,
                    DateTime CreatedDate,
                    bool IsDeleted);
