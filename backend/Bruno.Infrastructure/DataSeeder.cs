using Bruno.Domain.Entities;
using Bruno.Domain.Enums;
using Bruno.Domain.ValueObjects;
using Bruno.Infrastructure.Context;

namespace Bruno.Infrastructure;

public static class DataSeeder
{
	public static async Task SeedAsync(BrunoContext context)
	{
		if (context.Vehicles.Any())
			return;

		var vehicles = new List<Vehicle>
		{
			new() { RegistrationNumber = "AB12CDE", Make = "Toyota", Model = "Corolla", Year = 2021, DailyRate = 45m },
			new() { RegistrationNumber = "XY34FGH", Make = "Ford",   Model = "Focus",   Year = 2020, DailyRate = 40m },
			new() { RegistrationNumber = "MN56JKL", Make = "BMW",    Model = "3 Series", Year = 2022, DailyRate = 85m },
		};

		var customers = new List<Customer>
		{
			new() { FirstName = "Jane",  LastName = "Smith",  Email = "jane.smith@example.com",  PhoneNumber = "0821234567" },
			new() { FirstName = "John",  LastName = "Doe",    Email = "john.doe@example.com",    PhoneNumber = "0839876543" },
		};

		await context.Vehicles.AddRangeAsync(vehicles);
		await context.Customers.AddRangeAsync(customers);
		await context.SaveChangesAsync();

		var bookings = new List<Booking>
		{
			new()
			{
				DateRange  = new DateRange(DateTime.UtcNow.AddDays(5), DateTime.UtcNow.AddDays(8)),
				TotalPrice = vehicles[0].DailyRate * 3,
				Status     = BookingStatus.Active,
				VehicleId  = vehicles[0].Id,
				CustomerId = customers[0].Id
			},
			new()
			{
				DateRange  = new DateRange(DateTime.UtcNow.AddDays(10), DateTime.UtcNow.AddDays(12)),
				TotalPrice = vehicles[1].DailyRate * 2,
				Status     = BookingStatus.Active,
				VehicleId  = vehicles[1].Id,
				CustomerId = customers[1].Id
			}
		};

		await context.Bookings.AddRangeAsync(bookings);
		await context.SaveChangesAsync();
	}
}
