using Bruno.Application.DTOs;
using Bruno.Application.DTOs.Booking;
using Bruno.Application.Handlers.Booking.Create;
using Bruno.Application.Handlers.Booking.Delete;
using Bruno.Application.Handlers.Booking.Get;
using Bruno.Application.Handlers.Booking.List;
using Bruno.Application.Handlers.Booking.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bruno.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
	private readonly IMediator mediator;

	public BookingController(IMediator mediator)
	{
		this.mediator = mediator;
	}

	[HttpPost]
	public async Task<IActionResult> Create(CreateBookingDto dto)
	{
		var command = new CreateBookingCommand(dto.StartDate, dto.EndDate, dto.TotalPrice, dto.Status, dto.VehicleId, dto.CustomerId);

		var result = await mediator.Send(command);
		return Ok(result);
	}

	[HttpDelete]
	public async Task<IActionResult> Delete(Guid id)
	{
		var command = new DeleteBookingCommand(id);

		await mediator.Send(command);
		return NoContent();
	}

	[HttpGet]
	public async Task<IActionResult> Get(Guid id)
	{
		var command = new GetBookingCommand(id);

		var result = await mediator.Send(command);
		return Ok(result);
	}

	[HttpPost("list")]
	public async Task<IActionResult> List(ListDto dto)
	{
		var command = new ListBookingCommand(dto.Skip, dto.Take);

		var result = await mediator.Send(command);
		return Ok(result);
	}

	[HttpPut]
	public async Task<IActionResult> Update(UpdateBookingDto dto)
	{
		var command = new UpdateBookingCommand(dto.Id, dto.StartDate, dto.EndDate, dto.TotalPrice,
								dto.Status, dto.VehicleId, dto.CustomerId);

		await mediator.Send(command);
		return NoContent();
	}
}
