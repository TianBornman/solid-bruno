using Bruno.Application.Features.Booking.Queries.Get;
using Bruno.Domain.Entities;
using Bruno.Domain.Enums;
using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using Bruno.Domain.ValueObjects;
using Moq;

namespace Bruno.Tests.Queries;

public class GetBookingQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> uow;
    private readonly Mock<IBookingRepository> bookingRepository;
    private readonly GetBookingQueryHandler handler;

    public GetBookingQueryHandlerTests()
    {
        uow = new Mock<IUnitOfWork>();
        bookingRepository = new Mock<IBookingRepository>();

        uow.Setup(u => u.BookingRepository).Returns(bookingRepository.Object);

        handler = new GetBookingQueryHandler(uow.Object);
    }

    [Fact]
    public async Task Handle_WhenBookingExists_ReturnsBookingDto()
    {
        var vehicle = new Vehicle
        {
            RegistrationNumber = "AB12CDE",
            Make = "Honda",
            Model = "Civic",
            Year = 2019,
            DailyRate = 45m
        };

        var customer = new Customer
        {
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane@example.com",
            PhoneNumber = "+1234567890"
        };

        var booking = new Booking
        {
            DateRange = new DateRange(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(3)),
            TotalPrice = 90m,
            Status = BookingStatus.Active,
            Vehicle = vehicle,
            Customer = customer
        };

        bookingRepository.Setup(r => r.Get(booking.Id)).ReturnsAsync(booking);

        var query = new GetBookingQuery(booking.Id);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(booking.Id, result.Id);
        Assert.Equal(90m, result.TotalPrice);
        Assert.Equal(BookingStatus.Active, result.Status);
        Assert.Equal(vehicle.Id, result.VehicleId);
        Assert.Equal(customer.Id, result.CustomerId);
    }

    [Fact]
    public async Task Handle_WhenBookingNotFound_ThrowsNotFoundException()
    {
        var missingId = Guid.NewGuid();

        bookingRepository.Setup(r => r.Get(missingId)).ReturnsAsync((Booking?)null);

        var query = new GetBookingQuery(missingId);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(query, CancellationToken.None));
    }
}
