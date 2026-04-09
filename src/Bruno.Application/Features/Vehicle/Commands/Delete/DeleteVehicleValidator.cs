using FluentValidation;

namespace Bruno.Application.Features.Vehicle.Commands.Delete;

public class DeleteVehicleValidator : AbstractValidator<DeleteVehicleCommand>
{
	public DeleteVehicleValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty();
	}
}
