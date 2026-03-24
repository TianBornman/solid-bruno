using Bruno.Application.DTOs;
using Bruno.Application.DTOs.Customer;
using Bruno.Application.Features.Customer.Commands.Create;
using Bruno.Application.Features.Customer.Commands.Delete;
using Bruno.Application.Features.Customer.Commands.Update;
using Bruno.Application.Features.Customer.Queries.Get;
using Bruno.Application.Features.Customer.Queries.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bruno.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
	private readonly IMediator mediator;

	public CustomerController(IMediator mediator)
	{
		this.mediator = mediator;
	}

	[HttpPost]
	public async Task<IActionResult> Create(CreateCustomerDto dto)
	{
		var command = new CreateCustomerCommand(dto.FirstName, dto.LastName, dto.Email, dto.PhoneNumber);

		var result = await mediator.Send(command);
		return Ok(result);
	}

	[HttpDelete]
	public async Task<IActionResult> Delete(Guid id)
	{
		var command = new DeleteCustomerCommand(id);

		await mediator.Send(command);
		return NoContent();
	}

	[HttpGet]
	public async Task<IActionResult> Get(Guid id)
	{
		var command = new GetCustomerQuery(id);

		var result = await mediator.Send(command);
		return Ok(result);
	}

	[HttpPost("list")]
	public async Task<IActionResult> List(ListDto dto)
	{
		var command = new ListCustomerQuery(dto.Skip, dto.Take);

		var result = await mediator.Send(command);
		return Ok(result);
	}

	[HttpPut]
	public async Task<IActionResult> Update(UpdateCustomerDto dto)
	{
		var command = new UpdateCustomerCommand(dto.Id, dto.FirstName, dto.LastName, dto.Email, dto.PhoneNumber);

		await mediator.Send(command);
		return NoContent();
	}
}
