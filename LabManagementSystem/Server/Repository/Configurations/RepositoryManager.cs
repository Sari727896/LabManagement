using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.Implementations;
using Repository.Interfaces;
using Repository.Models;


namespace Repository.Configurations
{
    public class RepositoryManager
    {
        public ICustomerRepository CustomerRepository { get; set; }
        public IDeviceRepository DeviceRepository { get; set; }
        public IOrderRepository OrderRepository { get; set; }
        public IDeviceTypeRepository DeviceTypeRepository { get; set; }
        public IStatusRepository StatusRepository { get; set; }


        public RepositoryManager(string connectionString)
        {
            var services = new ServiceCollection();

            services.AddSingleton<LabContext>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IDeviceTypeRepository, DeviceTypeRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();

            services.AddDbContext<LabContext>(opt => opt.UseSqlServer(connectionString));

            var serviceProvider = services.BuildServiceProvider();

            CustomerRepository = serviceProvider.GetRequiredService<ICustomerRepository>();
            DeviceRepository = serviceProvider.GetRequiredService<IDeviceRepository>();
            OrderRepository = serviceProvider.GetRequiredService<IOrderRepository>();
            DeviceTypeRepository= serviceProvider.GetRequiredService<IDeviceTypeRepository>();
            StatusRepository=serviceProvider.GetRequiredService<IStatusRepository>();
        }
    }
}
