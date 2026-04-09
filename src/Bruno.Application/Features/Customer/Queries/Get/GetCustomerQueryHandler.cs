using Bruno.Application.DTOs.Customer;
using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Features.Customer.Queries.Get;

public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, GetCustomerDto?>
{
	private readonly IUnitOfWork uow;

	public GetCustomerQueryHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task<GetCustomerDto?> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
	{
		var entity = await uow.CustomerRepository.Get(request.Id) 
			?? throw new NotFoundException($"Customer not found."); ;

		var dto = new GetCustomerDto(entity.Id, entity.FirstName, entity.LastName, entity.Email, entity.PhoneNumber, entity.CreatedAt);

		return dto;
	}
}
