using Repository.Models;


namespace Repository.Interfaces
{
    public interface IDeviceRepository : IRepository<Device>
    {
        Task<Device?> GetByModelAsync(string model); 
        Task<Device?> GetByIdAsync(int deviceId);
    }
}
