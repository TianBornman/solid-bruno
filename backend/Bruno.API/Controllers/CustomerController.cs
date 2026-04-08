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

	/// <summary>Creates a new customer.</summary>
	/// <response code="200">Returns the ID of the created customer.</response>
	/// <response code="400">Validation failed.</response>
	[HttpPost]
	[ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Create(CreateCustomerDto dto)
	{
		var command = new CreateCustomerCommand(dto.FirstName, dto.LastName, dto.Email, dto.PhoneNumber);
		var result = await mediator.Send(command);
		return Ok(result);
	}

	/// <summary>Gets a customer by ID.</summary>
	/// <response code="200">Returns the customer.</response>
	/// <response code="404">Customer not found.</response>
	[HttpGet("{id}")]
	[ProducesResponseType(typeof(GetCustomerDto), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Get(Guid id)
	{
		var result = await mediator.Send(new GetCustomerQuery(id));
		return Ok(result);
	}

	/// <summary>Lists customers with optional search filter across name and email.</summary>
	/// <response code="200">Returns a list of customers.</response>
	[HttpPost("list")]
	[ProducesResponseType(typeof(List<GetCustomerDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> List(ListCustomerDto dto)
	{
		var result = await mediator.Send(new ListCustomerQuery(dto.Skip, dto.Take, dto.Search));
		return Ok(result);
	}

	/// <summary>Updates an existing customer.</summary>
	/// <response code="204">Customer updated.</response>
	/// <response code="400">Validation failed.</response>
	/// <response code="404">Customer not found.</response>
	[HttpPut]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Update(UpdateCustomerDto dto)
	{
		await mediator.Send(new UpdateCustomerCommand(dto.Id, dto.FirstName, dto.LastName, dto.Email, dto.PhoneNumber));
		return NoContent();
	}

	/// <summary>Deletes a customer. Fails if the customer has any bookings.</summary>
	/// <response code="204">Customer deleted.</response>
	/// <response code="400">Customer has existing bookings.</response>
	/// <response code="404">Customer not found.</response>
	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Delete(Guid id)
	{
		await mediator.Send(new DeleteCustomerCommand(id));
		return NoContent();
	}
}
