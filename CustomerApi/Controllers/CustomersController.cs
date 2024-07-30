using CustomerApi.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System;

// CustomersController.cs
[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }
    //[Route("customers")]
    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateDto customerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var customer = await _customerService.CreateCustomerAsync(customerDto.FullName, customerDto.DateOfBirth);
        return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null)
            return NotFound();

        return Ok(customer);
    }
    [HttpGet("{age:int}")]
    public async Task<IActionResult> GetCustomersByAge(int age)
    {
        var customers = await _customerService.GetCustomersByAgeAsync(age);
        return Ok(customers);
    }
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] CustomerUpdateDto customerDto)
    {
        try
        {
            var customer = await _customerService.UpdateCustomerAsync(id, customerDto.FullName, customerDto.DateOfBirth);
            return Ok(customer);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
