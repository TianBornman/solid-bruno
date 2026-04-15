using Bruno.Domain.Entities;
using Bruno.Domain.Exceptions;

namespace Bruno.Tests.Domain;

public class VehicleBusinessRuleTests
{
    private static Vehicle MakeVehicle(bool isDeleted = false) => new()
    {
        RegistrationNumber = "AB12CDE",
        Make = "Honda",
        Model = "Civic",
        Year = 2020,
        DailyRate = 50m,
        IsDeleted = isDeleted
    };

    [Fact]
    public void CanBook_WhenVehicleIsDeleted_ThrowsDomainException()
    {
        var vehicle = MakeVehicle(isDeleted: true);

        Assert.Throws<DomainException>(() => vehicle.CanBook());
    }

    [Fact]
    public void CanBook_WhenVehicleIsNotDeleted_DoesNotThrow()
    {
        var vehicle = MakeVehicle(isDeleted: false);

        // Should not throw
        vehicle.CanBook();
    }

    [Fact]
    public void SoftDelete_SetsIsDeletedToTrue()
    {
        var vehicle = MakeVehicle(isDeleted: false);

        vehicle.SoftDelete();

        Assert.True(vehicle.IsDeleted);
    }

    [Fact]
    public void Update_UpdatesAllFields()
    {
        var vehicle = MakeVehicle();

        vehicle.Update("XY99ZZZ", "Toyota", "Corolla", 2022, 75m);

        Assert.Equal("XY99ZZZ", vehicle.RegistrationNumber);
        Assert.Equal("Toyota", vehicle.Make);
        Assert.Equal("Corolla", vehicle.Model);
        Assert.Equal(2022, vehicle.Year);
        Assert.Equal(75m, vehicle.DailyRate);
    }
}
