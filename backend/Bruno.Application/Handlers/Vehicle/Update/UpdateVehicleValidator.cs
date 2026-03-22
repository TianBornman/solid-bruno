using FluentValidation;

namespace Bruno.Application.Handlers.Vehicle.Update;

public class UpdateVehicleValidator : AbstractValidator<UpdateVehicleCommand>
{
	public UpdateVehicleValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty();

		RuleFor(x => x.RegistrationNumber)
			.NotEmpty()
			.MaximumLength(8);

		RuleFor(x => x.Make)
			.NotEmpty()
			.MaximumLength(50);

		RuleFor(x => x.Model)
			.NotEmpty()
			.MaximumLength(50);

		RuleFor(x => x.Year)
			.NotEmpty()
			.GreaterThanOrEqualTo(1886);

		RuleFor(x => x.DailyRate)
			.NotEmpty()
			.GreaterThan(0);
	}
}
