using Service.DTOs;

namespace Service.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto?> GetCustomerByPhoneNumberAsync(string phoneNumber);
        Task<int> AddCustomerAsync(CustomerDto customerDto);
    }
}
