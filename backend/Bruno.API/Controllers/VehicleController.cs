using Bruno.Application.DTOs;
using Bruno.Application.DTOs.Vehicle;
using Bruno.Application.Handlers.Vehicle.Create;
using Bruno.Application.Handlers.Vehicle.Delete;
using Bruno.Application.Handlers.Vehicle.Get;
using Bruno.Application.Handlers.Vehicle.List;
using Bruno.Application.Handlers.Vehicle.Update;
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

	[HttpPost]
	public async Task<IActionResult> Create(CreateVehicleDto dto)
	{
		var command = new CreateVehicleCommand(dto.RegistrationNumber, dto.Make, dto.Model, dto.Year, dto.DailyRate);

		var result = await mediator.Send(command);
		return Ok(result);
	}

	[HttpDelete]
	public async Task<IActionResult> Delete(Guid id)
	{
		var command = new DeleteVehicleCommand(id);

		await mediator.Send(command);
		return NoContent();
	}

	[HttpGet]
	public async Task<IActionResult> Get(Guid id)
	{
		var command = new GetVehicleCommand(id);

		var result = await mediator.Send(command);
		return Ok(result);
	}

	[HttpPost("list")]
	public async Task<IActionResult> List(ListDto dto)
	{
		var command = new ListVehicleCommand(dto.Skip, dto.Take);

		var result = await mediator.Send(command);
		return Ok(result);
	}

	[HttpPut] 
	public async Task<IActionResult> Update(UpdateVehicleDto dto)
	{
		var command = new UpdateVehicleCommand(dto.Id, dto.RegistrationNumber, dto.Make, dto.Model, dto.Year, dto.DailyRate);

		await mediator.Send(command);
		return NoContent();
	}
}
