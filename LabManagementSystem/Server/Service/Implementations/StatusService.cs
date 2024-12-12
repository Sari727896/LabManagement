using AutoMapper;
using Repository.Configurations;
using Repository.Interfaces;
using Service.DTOs;
using Service.Interfaces;

namespace Service.Implementations
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public StatusService(RepositoryManager repositoryManager, IMapper mapper)
        {
            _statusRepository = repositoryManager.StatusRepository;
            _mapper = mapper;
        }

        public async Task<List<StatusDto>> GetAllStatusesAsync()
        {
            var statuses = await _statusRepository.GetAllActiveStatusesAsync();
            return _mapper.Map<List<StatusDto>>(statuses);
        }
    }
}
