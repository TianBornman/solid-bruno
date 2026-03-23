using FluentValidation;

namespace Bruno.Application.Handlers.Customer.List;

public class ListCustomerValidator : AbstractValidator<ListCustomerCommand>
{
	public ListCustomerValidator()
	{
		RuleFor(x => x.Skip)
			.GreaterThanOrEqualTo(0);

		RuleFor(x => x.Take)
			.GreaterThan(0);
	}
}
