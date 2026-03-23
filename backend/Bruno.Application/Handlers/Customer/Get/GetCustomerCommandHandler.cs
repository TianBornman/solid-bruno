using Bruno.Application.DTOs.Customer;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Handlers.Customer.Get;

public class GetCustomerCommandHandler : IRequestHandler<GetCustomerCommand, GetCustomerDto?>
{
	private readonly IUnitOfWork uow;

	public GetCustomerCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task<GetCustomerDto?> Handle(GetCustomerCommand request, CancellationToken cancellationToken)
	{
		var entity = await uow.CustomerRepository.Get(request.Id);

		if (entity == null) 
			return null;

		var dto = new GetCustomerDto(entity.Id, entity.FirstName, entity.LastName, entity.Email, entity.PhoneNumber, entity.CreatedAt);

		return dto;
	}
}
