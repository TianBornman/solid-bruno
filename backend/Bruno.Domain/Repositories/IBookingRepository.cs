using Bruno.Domain.Entities;
using Bruno.Domain.ValueObjects;

namespace Bruno.Domain.Repositories;

public interface IBookingRepository : IRepository<Booking>
{
	Task<bool> HasOverlappingBookingAsync(Guid vehicleId, DateRange dateRange);
	Task<bool> ExistsForCustomerAsync(Guid customerId);
}