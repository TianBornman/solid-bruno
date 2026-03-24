using FluentValidation;

namespace Bruno.Application.Features.Vehicle.Queries.List;

public class ListVehicleValidator : AbstractValidator<ListVehicleQuery>
{
	public ListVehicleValidator()
	{
		RuleFor(x => x.Skip)
			.GreaterThanOrEqualTo(0);

		RuleFor(x => x.Take)
			.GreaterThan(0);
	}
}
