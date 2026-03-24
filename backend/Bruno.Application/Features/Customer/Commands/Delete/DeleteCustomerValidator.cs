using FluentValidation;

namespace Bruno.Application.Features.Customer.Commands.Delete;

public class DeleteCustomerValidator : AbstractValidator<DeleteCustomerCommand>
{
	public DeleteCustomerValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty();
	}
}
