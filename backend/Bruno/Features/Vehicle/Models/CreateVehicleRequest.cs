namespace Bruno.Features.Vehicle.Models;

public record CreateVehicleRequest(string RegistrationNumber,
                            string Make,
                            string Model,
                            int Year,
                            decimal DailyRate);
