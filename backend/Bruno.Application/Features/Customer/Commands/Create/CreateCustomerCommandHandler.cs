using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Features.Customer.Commands.Create;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
{
	private readonly IUnitOfWork uow;

	public CreateCustomerCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
	{
		var entity = new Domain.Entities.Customer
		{
			FirstName = request.FirstName,
			LastName = request.LastName,
			Email = request.Email,
			PhoneNumber = request.PhoneNumber
		};

		await uow.CustomerRepository.Add(entity);

		return entity.Id;
	}
}
