using FluentValidation;

namespace Bruno.Application.Handlers.Customer.Update;

public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
{
	public UpdateCustomerValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty();

		RuleFor(x => x.FirstName)
			.NotEmpty()
			.MaximumLength(100);

		RuleFor(x => x.LastName)
			.NotEmpty()
			.MaximumLength(100);

		RuleFor(x => x.Email)
			.NotEmpty()
			.MaximumLength(150);

		RuleFor(x => x.PhoneNumber)
			.NotEmpty()
			.MaximumLength(20);
	}
}
