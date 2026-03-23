using FluentValidation;

namespace Bruno.Application.Handlers.Booking.Get;

public class GetBookingValidator : AbstractValidator<GetBookingCommand>
{
	public GetBookingValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty();
	}
}
