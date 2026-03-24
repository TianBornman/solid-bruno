using Bruno.Application.DTOs.Customer;
using Bruno.Application.DTOs.Vehicle;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Features.Customer.Queries.List;

public class ListCustomerQueryHandler : IRequestHandler<ListCustomerQuery, List<GetCustomerDto>>
{
	private readonly IUnitOfWork uow;

	public ListCustomerQueryHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task<List<GetCustomerDto>> Handle(ListCustomerQuery request, CancellationToken cancellationToken)
	{
		var entities = await uow.CustomerRepository.List(request.Skip, request.Take);

		var dtos = entities.Any() ? entities.Select(entity => 
									new GetCustomerDto(entity.Id, entity.FirstName, entity.LastName, 
									entity.Email, entity.PhoneNumber, entity.CreatedAt)).ToList() : [];

		return dtos;
	}
}
