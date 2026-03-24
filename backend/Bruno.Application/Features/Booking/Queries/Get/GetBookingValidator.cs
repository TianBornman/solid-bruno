using FluentValidation;

namespace Bruno.Application.Features.Booking.Queries.Get;

public class GetBookingValidator : AbstractValidator<GetBookingQuery>
{
	public GetBookingValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty();
	}
}
