using Bruno.Application.Features.Customer.Commands.Delete;
using Bruno.Domain.Entities;
using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using Moq;

namespace Bruno.Tests.Commands;

public class DeleteCustomerCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> uow;
    private readonly Mock<ICustomerRepository> customerRepository;
    private readonly Mock<IBookingRepository> bookingRepository;
    private readonly DeleteCustomerCommandHandler handler;

    public DeleteCustomerCommandHandlerTests()
    {
        uow = new Mock<IUnitOfWork>();
        customerRepository = new Mock<ICustomerRepository>();
        bookingRepository = new Mock<IBookingRepository>();

        uow.Setup(u => u.CustomerRepository).Returns(customerRepository.Object);
        uow.Setup(u => u.BookingRepository).Returns(bookingRepository.Object);

        handler = new DeleteCustomerCommandHandler(uow.Object);
    }

    [Fact]
    public async Task Handle_WhenCustomerNotFound_ThrowsNotFoundException()
    {
        var missingId = Guid.NewGuid();

        customerRepository.Setup(r => r.Get(missingId)).ReturnsAsync((Customer?)null);

        var command = new DeleteCustomerCommand(missingId);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenCustomerHasBookings_ThrowsDomainException()
    {
        var customer = new Customer
        {
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane@example.com",
            PhoneNumber = "+1234567890"
        };

        customerRepository.Setup(r => r.Get(customer.Id)).ReturnsAsync(customer);
        bookingRepository.Setup(r => r.ExistsForCustomerAsync(customer.Id)).ReturnsAsync(true);

        var command = new DeleteCustomerCommand(customer.Id);

        await Assert.ThrowsAsync<DomainException>(() =>
            handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenCustomerHasNoBookings_DeletesCustomer()
    {
        var customer = new Customer
        {
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane@example.com",
            PhoneNumber = "+1234567890"
        };

        customerRepository.Setup(r => r.Get(customer.Id)).ReturnsAsync(customer);
        bookingRepository.Setup(r => r.ExistsForCustomerAsync(customer.Id)).ReturnsAsync(false);
        customerRepository.Setup(r => r.Delete(customer)).Returns(Task.CompletedTask);

        var command = new DeleteCustomerCommand(customer.Id);

        await handler.Handle(command, CancellationToken.None);

        customerRepository.Verify(r => r.Delete(customer), Times.Once);
        uow.Verify(u => u.SaveChanges(), Times.Once);
    }
}
