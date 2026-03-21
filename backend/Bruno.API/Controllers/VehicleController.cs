using Bruno.Application.DTOs.Vehicle;
using Bruno.Application.Handlers.Vehicle.Create;
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
}
