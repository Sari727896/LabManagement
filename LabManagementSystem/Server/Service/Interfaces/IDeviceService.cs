using Service.DTOs;

namespace Service.Interfaces
{
    public interface IDeviceService
    {
        Task<DeviceDto?> GetDeviceByModelAsync(string model);
        Task<int> AddDeviceAsync(DeviceDto deviceDto);
        Task<IEnumerable<DeviceTypeDto>> GetDeviceTypesAsync();
    }
}
