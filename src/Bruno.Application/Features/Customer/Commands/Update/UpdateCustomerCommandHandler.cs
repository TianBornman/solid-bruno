using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Features.Customer.Commands.Update;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
	private readonly IUnitOfWork uow;

	public UpdateCustomerCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
	{
		var entity = await uow.CustomerRepository.Get(request.Id) 
			?? throw new NotFoundException($"Customer not found.");

		entity.Update(request.FirstName, request.LastName, request.Email, request.PhoneNumber);

		await uow.CustomerRepository.Update(entity);
		await uow.SaveChanges();
	}
}
