namespace Bruno.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
	public IBookingRepository BookingRepository { get; }
	public ICustomerRepository CustomerRepository { get; }
	public IVehicleRepository VehicleRepository { get; }

	public Task SaveChanges();
}
