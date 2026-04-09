using FluentValidation;

namespace Bruno.Application.Features.Customer.Queries.Get;

public class GetCustomerValidator : AbstractValidator<GetCustomerQuery>
{
	public GetCustomerValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty();
	}
}
