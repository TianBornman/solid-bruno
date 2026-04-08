using Bruno.Domain.Entities;
using Bruno.Domain.Enums;
using Bruno.Domain.ValueObjects;

namespace Bruno.Domain.Repositories;

public interface IBookingRepository : IRepository<Booking>
{
	Task<bool> HasOverlappingBookingAsync(Guid vehicleId, DateRange dateRange);
	Task<bool> ExistsForCustomerAsync(Guid customerId);
	Task<IEnumerable<Booking>> ListFiltered(int skip, int take, BookingStatus? status, string? search);
}
