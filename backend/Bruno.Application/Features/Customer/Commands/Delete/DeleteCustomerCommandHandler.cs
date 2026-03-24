using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Features.Customer.Commands.Delete;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
	private readonly IUnitOfWork uow;

	public DeleteCustomerCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
	{
		var entity = await uow.CustomerRepository.Get(request.Id) 
			?? throw new NotFoundException($"Customer not found.");

		var hasBookings = await uow.BookingRepository.ExistsForCustomerAsync(entity.Id);

		if (hasBookings)
			throw new DomainException("Customer cannot be deleted because bookings exist.");

		await uow.CustomerRepository.Delete(entity);
	}
}
