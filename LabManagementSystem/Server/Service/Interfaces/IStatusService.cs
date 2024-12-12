using Service.DTOs;

namespace Service.Interfaces
{
    public interface IStatusService
    {
        Task<List<StatusDto>> GetAllStatusesAsync();

    }
}
