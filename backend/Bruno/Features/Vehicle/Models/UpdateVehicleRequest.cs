namespace Bruno.Features.Vehicle.Models;

public record UpdateVehicleRequest(Guid Id,
                            string RegistrationNumber,
                            string Make,
                            string Model,
                            int Year,
                            decimal DailyRate);
