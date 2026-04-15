using Bruno.Application.Features.Customer.Queries.Get;
using Bruno.Domain.Entities;
using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using Moq;

namespace Bruno.Tests.Queries;

public class GetCustomerQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> uow;
    private readonly Mock<ICustomerRepository> customerRepository;
    private readonly GetCustomerQueryHandler handler;

    public GetCustomerQueryHandlerTests()
    {
        uow = new Mock<IUnitOfWork>();
        customerRepository = new Mock<ICustomerRepository>();

        uow.Setup(u => u.CustomerRepository).Returns(customerRepository.Object);

        handler = new GetCustomerQueryHandler(uow.Object);
    }

    [Fact]
    public async Task Handle_WhenCustomerExists_ReturnsCustomerDto()
    {
        var customer = new Customer
        {
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane@example.com",
            PhoneNumber = "+1234567890"
        };

        customerRepository.Setup(r => r.Get(customer.Id)).ReturnsAsync(customer);

        var query = new GetCustomerQuery(customer.Id);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(customer.Id, result.Id);
        Assert.Equal("Jane", result.FirstName);
        Assert.Equal("jane@example.com", result.Email);
    }

    [Fact]
    public async Task Handle_WhenCustomerNotFound_ThrowsNotFoundException()
    {
        var missingId = Guid.NewGuid();

        customerRepository.Setup(r => r.Get(missingId)).ReturnsAsync((Customer?)null);

        var query = new GetCustomerQuery(missingId);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(query, CancellationToken.None));
    }
}
