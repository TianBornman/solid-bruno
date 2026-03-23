using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Handlers.Customer.Update;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
	private readonly IUnitOfWork uow;

	public UpdateCustomerCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
	{
		var entity = await uow.CustomerRepository.Get(request.Id);

		if (entity == null)
			return;

		entity.FirstName = request.FirstName;
		entity.LastName = request.LastName;
		entity.Email = request.Email;
		entity.PhoneNumber = request.PhoneNumber;

		await uow.CustomerRepository.Update(entity);
	}
}
