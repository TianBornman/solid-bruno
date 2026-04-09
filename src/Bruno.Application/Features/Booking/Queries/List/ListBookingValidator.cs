using FluentValidation;

namespace Bruno.Application.Features.Booking.Queries.List;

public class ListBookingValidator : AbstractValidator<ListBookingQuery>
{
	public ListBookingValidator()
	{
		RuleFor(x => x.Skip)
			.GreaterThanOrEqualTo(0);

		RuleFor(x => x.Take)
			.GreaterThan(0);
	}
}
