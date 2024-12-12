using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models;


namespace Repository.Implementations
{
    public class StatusRepository : IStatusRepository
    {
        private readonly LabContext _context;

        public StatusRepository(LabContext context)
        {
            _context = context;
        }

        public async Task<List<Status>> GetAllActiveStatusesAsync()
        {
            return await _context.Statuses
                .Where(s => s.IsActive == true && s.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<Status?> GetStatusByIdAsync(int statusId)
        {
            return await _context.Statuses
                .Where(s => s.StatusId == statusId && s.IsActive == true && s.IsDeleted == false)
                .FirstOrDefaultAsync();
        }
    }
}
