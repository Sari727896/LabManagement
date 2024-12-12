using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models;


namespace Repository.Implementations
{
    public class DeviceTypeRepository : IDeviceTypeRepository
    {
        private readonly LabContext _context;

        public DeviceTypeRepository(LabContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DeviceType>> GetAllActiveAsync()
        {
            return await _context.DeviceTypes
                .Where(dt => dt.IsActive == true && dt.IsDeleted == false)
                .ToListAsync();
        }
    }
}
