using FluentValidation;

namespace Bruno.Application.Handlers.Booking.Update;

public class UpdateBookingValidator : AbstractValidator<UpdateBookingCommand>
{
	public UpdateBookingValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty();

		RuleFor(x => x.StartDate)
			.LessThan(x => x.EndDate);

		RuleFor(x => x.EndDate)
			.GreaterThan(x => x.StartDate);

		RuleFor(x => x.TotalPrice)
			.NotEmpty()
			.GreaterThan(0);

		RuleFor(x => x.Status)
			.NotEmpty();

		RuleFor(x => x.CustomerId)
			.NotEmpty();

		RuleFor(x => x.VehicleId)
			.NotEmpty();
	}
}
