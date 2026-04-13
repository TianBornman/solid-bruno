using Bruno.Application.DTOs.Booking;
using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Features.Booking.Queries.Get;

public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, GetBookingDto?>
{
	private readonly IUnitOfWork uow;

	public GetBookingQueryHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task<GetBookingDto?> Handle(GetBookingQuery request, CancellationToken cancellationToken)
	{
		var entity = await uow.BookingRepository.Get(request.Id) 
			?? throw new NotFoundException($"Booking not found.");

		return GetBookingDto.FromEntity(entity);
	}
}
