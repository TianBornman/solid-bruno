using FluentValidation;

namespace Bruno.Application.Features.Customer.Commands.Create;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
	public CreateCustomerValidator()
	{
		RuleFor(x => x.FirstName)
			.NotEmpty()
			.MaximumLength(100);

		RuleFor(x => x.LastName)
			.NotEmpty()
			.MaximumLength(100);

		RuleFor(x => x.Email)
			.NotEmpty()
			.EmailAddress();

		RuleFor(x => x.PhoneNumber)
			.NotEmpty()
			.MaximumLength(20);
	}
}
