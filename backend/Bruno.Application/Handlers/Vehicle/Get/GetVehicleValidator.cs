using FluentValidation;

namespace Bruno.Application.Handlers.Vehicle.Get;

public class GetVehicleValidator : AbstractValidator<GetVehicleCommand>
{
	public GetVehicleValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty();
	}
}
