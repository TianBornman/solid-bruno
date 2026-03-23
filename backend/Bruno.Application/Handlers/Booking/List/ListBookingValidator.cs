using FluentValidation;

namespace Bruno.Application.Handlers.Booking.List;

public class ListBookingValidator : AbstractValidator<ListBookingCommand>
{
	public ListBookingValidator()
	{
		RuleFor(x => x.Skip)
			.GreaterThanOrEqualTo(0);

		RuleFor(x => x.Take)
			.GreaterThan(0);
	}
}
