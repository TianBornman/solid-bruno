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
		var entity = await uow.CustomerRepository.Get(request.Id);

		if (entity != null)
			await uow.CustomerRepository.Delete(entity);
	}
}
