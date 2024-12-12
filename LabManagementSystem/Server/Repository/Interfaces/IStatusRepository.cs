using Repository.Models;

namespace Repository.Interfaces
{
    public interface IStatusRepository
    {
        Task<List<Status>> GetAllActiveStatusesAsync();
        Task<Status?> GetStatusByIdAsync(int statusId);
    }
}
