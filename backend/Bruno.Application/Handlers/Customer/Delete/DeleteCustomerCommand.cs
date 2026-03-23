using MediatR;

namespace Bruno.Application.Handlers.Customer.Delete;

public record DeleteCustomerCommand(Guid Id) : IRequest;