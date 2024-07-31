using CustomerApi.DataAccess;
using System;

namespace CustomerApi.BusinessLogic
{

    public interface ICustomerService
    {
        Task<Customer> CreateCustomerAsync(string fullName, DateOnly dateOfBirth);
        Task<List<Customer>> GetCustomerList();
        Task<Customer> GetCustomerByIdAsync(Guid id);
        Task<IEnumerable<Customer>> GetCustomersByAgeAsync(int age);
        Task<Customer> UpdateCustomerAsync(Guid id, string newName, DateOnly? newDateOfBirth);
    }
}