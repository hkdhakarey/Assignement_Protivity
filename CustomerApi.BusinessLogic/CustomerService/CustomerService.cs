using CustomerApi.DataAccess;

namespace CustomerApi.BusinessLogic
{

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly HttpClient _httpClient;

        public CustomerService(ICustomerRepository customerRepository, HttpClient httpClient)
        {
            _customerRepository = customerRepository;
            _httpClient = httpClient;
        }

        public async Task<Customer> CreateCustomerAsync(string fullName, DateOnly dateOfBirth)
        {
            var customer = new Customer
            {
                CustomerId = Guid.NewGuid(),
                FullName = fullName,
                DateOfBirth = dateOfBirth,
                ProfileImage = String.Empty
            };

            // Call API to generate profile image
            // var profileImageUrl = $"https://ui-avatars.com/api/?name={Uri.EscapeDataString(fullName)}&format=svg";
            // customer.ProfileImage = await _httpClient.GetStringAsync(profileImageUrl);

            return await _customerRepository.AddCustomerAsync(customer);
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            return await _customerRepository.GetCustomerByIdAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetCustomersByAgeAsync(int age)
        {
            return await _customerRepository.GetCustomersByAgeAsync(age);
        }

        public async Task<Customer> UpdateCustomerAsync(Guid id, string newName, DateOnly? newDateOfBirth)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found");

            if (!string.IsNullOrEmpty(newName))
            {
                customer.FullName = newName;

                // Update profile image
                var profileImageUrl = $"https://ui-avatars.com/api/?name={Uri.EscapeDataString(newName)}&format=svg";
                customer.ProfileImage = await _httpClient.GetStringAsync(profileImageUrl);
            }

            if (newDateOfBirth.HasValue)
            {
                customer.DateOfBirth = newDateOfBirth.Value;
            }

            return await _customerRepository.UpdateCustomerAsync(customer);
        }
    }
}