using Bruno.Application.DTOs.Vehicle;
using Bruno.Application.Features.Vehicle.Commands.Create;
using Bruno.Application.Features.Vehicle.Commands.Delete;
using Bruno.Application.Features.Vehicle.Commands.Update;
using Bruno.Application.Features.Vehicle.Queries.Get;
using Bruno.Application.Features.Vehicle.Queries.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bruno.API.Controllers;

[ApiController]
[Route("[controller]")]
public class VehicleController : ControllerBase
{
	private readonly IMediator mediator;

	public VehicleController(IMediator mediator)
	{
		this.mediator = mediator;
	}

	/// <summary>Creates a new vehicle.</summary>
	/// <response code="200">Returns the ID of the created vehicle.</response>
	/// <response code="400">Validation failed.</response>
	[HttpPost]
	[ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Create(CreateVehicleDto dto)
	{
		var command = new CreateVehicleCommand(dto.RegistrationNumber, dto.Make, dto.Model, dto.Year, dto.DailyRate);
		var result = await mediator.Send(command);
		return Ok(result);
	}

	/// <summary>Gets a vehicle by ID.</summary>
	/// <response code="200">Returns the vehicle.</response>
	/// <response code="404">Vehicle not found.</response>
	[HttpGet("{id}")]
	[ProducesResponseType(typeof(GetVehicleDto), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Get(Guid id)
	{
		var result = await mediator.Send(new GetVehicleQuery(id));
		return Ok(result);
	}

	/// <summary>Lists vehicles with optional search filter.</summary>
	/// <response code="200">Returns a list of vehicles.</response>
	[HttpPost("list")]
	[ProducesResponseType(typeof(List<GetVehicleDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> List(ListVehicleDto dto)
	{
		var result = await mediator.Send(new ListVehicleQuery(dto.Skip, dto.Take, dto.Search));
		return Ok(result);
	}

	/// <summary>Updates an existing vehicle.</summary>
	/// <response code="204">Vehicle updated.</response>
	/// <response code="400">Validation failed.</response>
	/// <response code="404">Vehicle not found.</response>
	[HttpPut]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Update(UpdateVehicleDto dto)
	{
		await mediator.Send(new UpdateVehicleCommand(dto.Id, dto.RegistrationNumber, dto.Make, dto.Model, dto.Year, dto.DailyRate));
		return NoContent();
	}

	/// <summary>Soft-deletes a vehicle.</summary>
	/// <response code="204">Vehicle deleted.</response>
	/// <response code="404">Vehicle not found.</response>
	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Delete(Guid id)
	{
		await mediator.Send(new DeleteVehicleCommand(id));
		return NoContent();
	}
}
