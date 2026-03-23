using FluentValidation;

namespace Bruno.Application.Handlers.Customer.Delete;

public class DeleteCustomerValidator : AbstractValidator<DeleteCustomerCommand>
{
	public DeleteCustomerValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty();
	}
}
