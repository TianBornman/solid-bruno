using FluentValidation;

namespace Bruno.Application.Handlers.Vehicle.Delete;

public class DeleteVehicleValidator : AbstractValidator<DeleteVehicleCommand>
{
	public DeleteVehicleValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty();
	}
}
