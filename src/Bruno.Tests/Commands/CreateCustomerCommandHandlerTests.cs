using Bruno.Application.Features.Customer.Commands.Create;
using Bruno.Domain.Entities;
using Bruno.Domain.Repositories;
using Moq;

namespace Bruno.Tests.Commands;

public class CreateCustomerCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> uow;
    private readonly Mock<ICustomerRepository> customerRepository;
    private readonly CreateCustomerCommandHandler handler;

    public CreateCustomerCommandHandlerTests()
    {
        uow = new Mock<IUnitOfWork>();
        customerRepository = new Mock<ICustomerRepository>();

        uow.Setup(u => u.CustomerRepository).Returns(customerRepository.Object);

        handler = new CreateCustomerCommandHandler(uow.Object);
    }

    [Fact]
    public async Task Handle_CreatesCustomerAndReturnsNonEmptyId()
    {
        customerRepository.Setup(r => r.Add(It.IsAny<Customer>())).Returns(Task.CompletedTask);

        var command = new CreateCustomerCommand("Jane", "Doe", "jane@example.com", "+1234567890");

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotEqual(Guid.Empty, result);
        customerRepository.Verify(r => r.Add(It.IsAny<Customer>()), Times.Once);
        uow.Verify(u => u.SaveChanges(), Times.Once);
    }
}
