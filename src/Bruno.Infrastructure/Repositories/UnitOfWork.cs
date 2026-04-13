using Bruno.Domain.Repositories;
using Bruno.Infrastructure.Context;

namespace Bruno.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
	private readonly BrunoContext dbContext;

	private IBookingRepository? _bookingRepository;
	private ICustomerRepository? _customerRepository;
	private IVehicleRepository? _vehicleRepository;

	public IBookingRepository BookingRepository => _bookingRepository ??= new BookingRepository(dbContext);
	public ICustomerRepository CustomerRepository => _customerRepository ??= new CustomerRepository(dbContext);
	public IVehicleRepository VehicleRepository => _vehicleRepository ??= new VehicleRepository(dbContext);

	public UnitOfWork(BrunoContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public void Dispose()
	{
		dbContext.Dispose();
	}

	public async Task SaveChanges()
	{
		await dbContext.SaveChangesAsync();
	}
}
