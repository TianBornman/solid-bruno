using Bruno.Application.DTOs.Booking;
using Bruno.Application.Features.Booking.Commands.Create;
using Bruno.Application.Features.Booking.Commands.Delete;
using Bruno.Application.Features.Booking.Commands.Update;
using Bruno.Application.Features.Booking.Queries.Get;
using Bruno.Application.Features.Booking.Queries.List;
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

	/// <summary>Creates a new booking. Validates date range and checks for overlaps.</summary>
	/// <response code="200">Returns the ID of the created booking.</response>
	/// <response code="400">Validation failed or booking overlaps an existing booking.</response>
	/// <response code="404">Vehicle or customer not found.</response>
	[HttpPost]
	[ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Create(CreateBookingDto dto)
	{
		var command = new CreateBookingCommand(dto.StartDate, dto.EndDate, dto.TotalPrice, dto.Status, dto.VehicleId, dto.CustomerId);
		var result = await mediator.Send(command);
		return Ok(result);
	}

	/// <summary>Gets a booking by ID.</summary>
	/// <response code="200">Returns the booking.</response>
	/// <response code="404">Booking not found.</response>
	[HttpGet("{id}")]
	[ProducesResponseType(typeof(GetBookingDto), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Get(Guid id)
	{
		var result = await mediator.Send(new GetBookingQuery(id));
		return Ok(result);
	}

	/// <summary>Lists bookings with optional filters by status, vehicle, or customer.</summary>
	/// <response code="200">Returns a list of bookings.</response>
	[HttpPost("list")]
	[ProducesResponseType(typeof(List<GetBookingDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> List(ListBookingDto dto)
	{
		var result = await mediator.Send(new ListBookingQuery(dto.Skip, dto.Take, dto.Status, dto.Search));
		return Ok(result);
	}

	/// <summary>Updates an existing booking.</summary>
	/// <response code="204">Booking updated.</response>
	/// <response code="400">Validation failed.</response>
	/// <response code="404">Booking not found.</response>
	[HttpPut]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Update(UpdateBookingDto dto)
	{
		await mediator.Send(new UpdateBookingCommand(dto.Id, dto.StartDate, dto.EndDate, dto.TotalPrice, dto.Status, dto.VehicleId, dto.CustomerId));
		return NoContent();
	}

	/// <summary>Deletes a booking. Only future bookings can be deleted.</summary>
	/// <response code="204">Booking deleted.</response>
	/// <response code="400">Booking is not in the future.</response>
	/// <response code="404">Booking not found.</response>
	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Delete(Guid id)
	{
		await mediator.Send(new DeleteBookingCommand(id));
		return NoContent();
	}
}
