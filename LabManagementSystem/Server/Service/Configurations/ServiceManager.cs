using Microsoft.Extensions.DependencyInjection;
using Repository.Configurations;
using Service.Implementations;
using Service.Interfaces;

namespace Service.Configurations
{
    public class ServiceManager
    {
        public ICustomerService Customers { get; set; }
        public IDeviceService Devices { get; set; }
        public IOrderService Orders { get; set; }
        public IStatusService Status { get; set; }
        public IICountService ICountService { get; set; }

        public ServiceManager(string connStr)
        {
            ServiceCollection services = new ServiceCollection();


            services.AddAutoMapper(typeof(AutoMapper.AutoMapperProfile));

            services.AddScoped<RepositoryManager>(x => new RepositoryManager(connStr));

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IICountService, ICountClientService>();
            services.AddScoped<HttpClient>(provider => new HttpClient());


            ServiceProvider provider = services.BuildServiceProvider();

            Customers = provider.GetRequiredService<ICustomerService>();
            Devices = provider.GetRequiredService<IDeviceService>();
            Orders = provider.GetRequiredService<IOrderService>();
            Status = provider.GetRequiredService<IStatusService>();
            ICountService = provider.GetRequiredService<IICountService>();
        }

    }
}
