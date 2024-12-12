using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models;


namespace Repository.Implementations
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly LabContext _context;

        public DeviceRepository(LabContext context)
        {
            _context = context;
        }

        public async Task<Device?> GetByModelAsync(string model)
        {
            return await _context.Devices
                                 .FirstOrDefaultAsync(d => d.Model == model &&
                                                           (d.IsActive ?? false) &&
                                                           !(d.IsDeleted ?? false));
        }

        public async Task<Device?> GetByIdAsync(int deviceId)
        {
            return await _context.Devices
                                 .Where(d => d.DeviceId == deviceId &&
                                             (d.IsActive ?? false) &&
                                             !(d.IsDeleted ?? false))
                                 .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Device device)
        {
            await _context.Devices.AddAsync(device);
            await _context.SaveChangesAsync();
        }
    }
}
