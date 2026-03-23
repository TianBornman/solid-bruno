using FluentValidation;

namespace Bruno.Application.Handlers.Customer.Get;

public class GetCustomerValidator : AbstractValidator<GetCustomerCommand>
{
	public GetCustomerValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty();
	}
}
