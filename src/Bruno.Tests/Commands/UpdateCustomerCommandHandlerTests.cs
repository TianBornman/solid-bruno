using Bruno.Application.Features.Customer.Commands.Update;
using Bruno.Domain.Entities;
using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using Moq;

namespace Bruno.Tests.Commands;

public class UpdateCustomerCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> uow;
    private readonly Mock<ICustomerRepository> customerRepository;
    private readonly UpdateCustomerCommandHandler handler;

    public UpdateCustomerCommandHandlerTests()
    {
        uow = new Mock<IUnitOfWork>();
        customerRepository = new Mock<ICustomerRepository>();

        uow.Setup(u => u.CustomerRepository).Returns(customerRepository.Object);

        handler = new UpdateCustomerCommandHandler(uow.Object);
    }

    [Fact]
    public async Task Handle_WhenCustomerExists_UpdatesCustomer()
    {
        var customer = new Customer
        {
            FirstName = "Old",
            LastName = "Name",
            Email = "old@example.com",
            PhoneNumber = "0000000000"
        };

        customerRepository.Setup(r => r.Get(customer.Id)).ReturnsAsync(customer);
        customerRepository.Setup(r => r.Update(customer)).Returns(Task.CompletedTask);

        var command = new UpdateCustomerCommand(customer.Id, "Jane", "Doe", "jane@example.com", "+1234567890");

        await handler.Handle(command, CancellationToken.None);

        Assert.Equal("Jane", customer.FirstName);
        Assert.Equal("Doe", customer.LastName);
        Assert.Equal("jane@example.com", customer.Email);
        uow.Verify(u => u.SaveChanges(), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenCustomerNotFound_ThrowsNotFoundException()
    {
        var missingId = Guid.NewGuid();

        customerRepository.Setup(r => r.Get(missingId)).ReturnsAsync((Customer?)null);

        var command = new UpdateCustomerCommand(missingId, "Jane", "Doe", "jane@example.com", "+1234567890");

        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(command, CancellationToken.None));
    }
}
