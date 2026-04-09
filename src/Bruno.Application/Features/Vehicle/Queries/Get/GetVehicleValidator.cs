using FluentValidation;

namespace Bruno.Application.Features.Vehicle.Queries.Get;

public class GetVehicleValidator : AbstractValidator<GetVehicleQuery>
{
	public GetVehicleValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty();
	}
}
