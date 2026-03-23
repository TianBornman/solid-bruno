using FluentValidation;

namespace Bruno.Application.Handlers.Booking.Delete;

public class DeleteBookingValidator : AbstractValidator<DeleteBookingCommand>
{
	public DeleteBookingValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty();
	}
}
