using MediatR;

namespace Bruno.Application.Features.Customer.Commands.Delete;

public record DeleteCustomerCommand(Guid Id) : IRequest;