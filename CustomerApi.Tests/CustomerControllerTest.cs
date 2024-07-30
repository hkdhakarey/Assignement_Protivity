using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CustomerApi.BusinessLogic;
using CustomerApi.DataAccess;
using CustomerApi;
namespace CustomerApi.Tests;
public class CustomersControllerTests
{
    private readonly Mock<ICustomerService> _customerServiceMock;
    private readonly CustomersController _controller;

    public CustomersControllerTests()
    {
        _customerServiceMock = new Mock<ICustomerService>();
        _controller = new CustomersController(_customerServiceMock.Object);
    }

    [Fact]
    public async Task CreateCustomer_ValidInput_ReturnsCreatedAtAction()
    {
        // Arrange
        var customerDto = new CustomerCreateDto
        {
            FullName = "Hari Krashna",
            DateOfBirth = new DateOnly(1995, 6, 15)
        };

        var createdCustomer = new Customer
        {
            CustomerId = Guid.NewGuid(),
            FullName = customerDto.FullName,
            DateOfBirth = customerDto.DateOfBirth
        };

        _customerServiceMock.Setup(service => service.CreateCustomerAsync(customerDto.FullName, customerDto.DateOfBirth))
                            .ReturnsAsync(createdCustomer);

        // Act
        var result = await _controller.CreateCustomer(customerDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnedCustomer = Assert.IsType<Customer>(createdAtActionResult.Value);
        Assert.Equal(createdCustomer.CustomerId, returnedCustomer.CustomerId);
    }
}
