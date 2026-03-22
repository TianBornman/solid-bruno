using FluentValidation;

namespace Bruno.Application.Handlers.Vehicle.List;

public class ListVehicleValidator : AbstractValidator<ListVehicleCommand>
{
	public ListVehicleValidator()
	{
		RuleFor(x => x.Skip)
			.GreaterThanOrEqualTo(0);

		RuleFor(x => x.Take)
			.GreaterThan(0);
	}
}
