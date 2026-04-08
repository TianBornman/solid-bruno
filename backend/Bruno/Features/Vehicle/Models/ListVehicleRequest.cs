namespace Bruno.Features.Vehicle.Models;

public record ListVehicleRequest(int Skip, int Take, string? Search);
