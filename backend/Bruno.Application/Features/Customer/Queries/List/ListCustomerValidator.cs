using FluentValidation;

namespace Bruno.Application.Features.Customer.Queries.List;

public class ListCustomerValidator : AbstractValidator<ListCustomerQuery>
{
	public ListCustomerValidator()
	{
		RuleFor(x => x.Skip)
			.GreaterThanOrEqualTo(0);

		RuleFor(x => x.Take)
			.GreaterThan(0);
	}
}
