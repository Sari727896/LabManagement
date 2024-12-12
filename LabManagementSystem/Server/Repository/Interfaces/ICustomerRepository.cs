using Repository.Models;

namespace Repository.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer?> GetByPhoneNumberAsync(string phoneNumber);
        Task<Customer?> GetByIdAsync(int customerId);
    }
}
