namespace CustomerApi.DataAccess;

public interface ICustomerRepository
{
    Task<Customer> AddCustomerAsync(Customer customer);
    Task<Customer> GetCustomerByIdAsync(Guid id);
    Task<IEnumerable<Customer>> GetCustomersByAgeAsync(int age);
    Task<Customer> UpdateCustomerAsync(Customer customer);
}