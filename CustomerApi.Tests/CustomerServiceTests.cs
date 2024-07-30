using CustomerApi.BusinessLogic;
using CustomerApi.DataAccess;
using FluentAssertions;
using Moq;

namespace CustomerApi.Tests;

public class CustomerServiceTests
{
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly HttpClient _httpClient;
    private readonly CustomerService _customerService;

    public CustomerServiceTests()
    {
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _httpClient = new HttpClient();
        _customerService = new CustomerService(_customerRepositoryMock.Object, _httpClient);
    }

    [Fact]
    public async Task CreateCustomer_ShouldReturnCustomerWithProfileImage()
    {
        // Arrange
        var fullName = "Hari Krashna";
        var dateOfBirth = new DateOnly(1990, 1, 1);

        // Act
        var customer = await _customerService.CreateCustomerAsync(fullName, dateOfBirth);

        // Assert
        customer.FullName.Should().Be(fullName);
        customer.DateOfBirth.Should().Be(dateOfBirth);
        customer.ProfileImage.Should().NotBeNullOrEmpty();
    }

    // More tests for other methods...
}
