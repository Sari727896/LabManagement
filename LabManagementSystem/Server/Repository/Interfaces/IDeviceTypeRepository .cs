using Repository.Models;


namespace Repository.Interfaces
{
    public interface IDeviceTypeRepository
    {
        Task<IEnumerable<DeviceType>> GetAllActiveAsync();
    }
}
