using Bruno.Application.DTOs.Customer;
using Bruno.Application.DTOs.Vehicle;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Handlers.Customer.List;

public class ListCustomerCommandHandler : IRequestHandler<ListCustomerCommand, List<GetCustomerDto>>
{
	private readonly IUnitOfWork uow;

	public ListCustomerCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task<List<GetCustomerDto>> Handle(ListCustomerCommand request, CancellationToken cancellationToken)
	{
		var entities = await uow.CustomerRepository.List(request.Skip, request.Take);

		var dtos = entities.Any() ? entities.Select(entity => 
									new GetCustomerDto(entity.Id, entity.FirstName, entity.LastName, 
									entity.Email, entity.PhoneNumber, entity.CreatedAt)).ToList() : [];

		return dtos;
	}
}
