using Microsoft.EntityFrameworkCore;

namespace CustomerApi.DataAccess
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        public async Task<List<Customer>> GetAllCustomer()
        {
            return await _context.Customers.ToListAsync();
        }
        public async Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetCustomersByAgeAsync(int age)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            return await _context.Customers
                .Where(c => (today.Year - c.DateOfBirth.Year) == age)
                .ToListAsync();
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
    }
}