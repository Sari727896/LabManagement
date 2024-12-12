using AutoMapper;
using Repository.Configurations;
using Repository.Interfaces;
using Repository.Models;
using Service.DTOs;
using Service.Interfaces;

namespace Service.Implementations
{
    public class DeviceService:IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IDeviceTypeRepository _deviceTypeRepository;
        private readonly IMapper _mapper;

        public DeviceService(RepositoryManager repositoryManager, IMapper mapper)
        {
            _deviceRepository = repositoryManager.DeviceRepository;
            _deviceTypeRepository = repositoryManager.DeviceTypeRepository;
            _mapper = mapper;
        }

        public async Task<DeviceDto?> GetDeviceByModelAsync(string model)
        {
            var device = await _deviceRepository.GetByModelAsync(model);
            return device == null ? null : _mapper.Map<DeviceDto>(device);
        }

        public async Task<int> AddDeviceAsync(DeviceDto deviceDto)
        {
            var existingDevice = await _deviceRepository.GetByModelAsync(deviceDto.Model);
            if (existingDevice != null)
            {
                return existingDevice.DeviceId;
            }

            var newDevice = _mapper.Map<Device>(deviceDto);
            await _deviceRepository.AddAsync(newDevice);
            return newDevice.DeviceId;
        }

        public async Task<IEnumerable<DeviceTypeDto>> GetDeviceTypesAsync()
        {
            var deviceTypes = await _deviceTypeRepository.GetAllActiveAsync();
            return _mapper.Map<IEnumerable<DeviceTypeDto>>(deviceTypes);
        }
    }
}
