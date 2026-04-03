namespace Bruno.Features.Vehicle.Models;

public class VehicleFormModel
{
    public string RegistrationNumber { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; } = DateTime.Now.Year;
    public decimal DailyRate { get; set; }

    public CreateVehicleRequest ToCreateRequest() =>
        new(RegistrationNumber.Trim().ToUpperInvariant(), Make.Trim(), Model.Trim(), Year, DailyRate);

    public UpdateVehicleRequest ToUpdateRequest(Guid id) =>
        new(id, RegistrationNumber.Trim().ToUpperInvariant(), Make.Trim(), Model.Trim(), Year, DailyRate);

    public static VehicleFormModel FromDto(VehicleDto dto) => new()
    {
        RegistrationNumber = dto.RegistrationNumber,
        Make = dto.Make,
        Model = dto.Model,
        Year = dto.Year,
        DailyRate = dto.DailyRate
    };
}
