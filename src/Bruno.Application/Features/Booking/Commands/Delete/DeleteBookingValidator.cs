using FluentValidation;

namespace Bruno.Application.Features.Booking.Commands.Delete;

public class DeleteBookingValidator : AbstractValidator<DeleteBookingCommand>
{
	public DeleteBookingValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty();
	}
}
