using AutoMapper;
using Repository.Configurations;
using Repository.Interfaces;
using Repository.Models;
using Service.DTOs;
using Service.Interfaces;

namespace Service.Implementations
{
    public class CustomerService:ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IICountService _iCountService;

        public CustomerService(RepositoryManager repositoryManager, IMapper mapper, IICountService iCountService)
        {
            _customerRepository = repositoryManager.CustomerRepository;
            _mapper = mapper;
            _iCountService = iCountService;
        }
        public async Task<CustomerDto?> GetCustomerByPhoneNumberAsync(string phoneNumber)
        {
            var customer = await _customerRepository.GetByPhoneNumberAsync(phoneNumber);
            return customer == null ? null : _mapper.Map<CustomerDto>(customer);
        }
        public async Task<int> AddCustomerAsync(CustomerDto customerDto)
        {
            var existingCustomer = await _customerRepository.GetByPhoneNumberAsync(customerDto.PhoneNumber);
            if (existingCustomer != null)
            {
                return existingCustomer.CustomerId; 
            }

            var newCustomer = _mapper.Map<Customer>(customerDto);
            var iCountId = await _iCountService.GetClientId(customerDto.PhoneNumber, customerDto.Email);
            if(iCountId > 0)
            {
                newCustomer.IcountId = iCountId;
            }
            await _customerRepository.AddAsync(newCustomer);
            return newCustomer.CustomerId; 
        }
    }
}
