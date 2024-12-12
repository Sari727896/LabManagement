using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models;


namespace Repository.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly LabContext _context;

        public CustomerRepository(LabContext context)
        {
            _context = context;
        }

        public async Task<Customer?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Customers
                                 .FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber &&
                                                           (c.IsActive ?? false) &&
                                                           !(c.IsDeleted ?? false));
        }

        public async Task<Customer?> GetByIdAsync(int customerId)
        {
            return await _context.Customers
                                 .Where(c => c.CustomerId == customerId &&
                                             (c.IsActive ?? false) &&
                                             !(c.IsDeleted ?? false))
                                 .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
    }
}
