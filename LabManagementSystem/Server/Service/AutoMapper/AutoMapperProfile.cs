using AutoMapper;
using Repository.Models;
using Service.DTOs;


namespace Service.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<DeviceDto, Device>()
                .ForMember(dest => dest.DeviceTypeTblId, opt => opt.MapFrom(src => GetDeviceTypeId(src.DeviceType)))
                .ReverseMap()
                .ForMember(dest => dest.DeviceType, opt => opt.MapFrom(src => src.DeviceTypeTbl.TypeName));

            CreateMap<DeviceType, DeviceTypeDto>().ReverseMap();

            CreateMap<OrderDto, Order>().ReverseMap();

            CreateMap<Order, OrderDetailsDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerTbl.FullName))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.StatusTbl.StatusName))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.DeviceTbl.DeviceTypeTbl.TypeName))
                .ForMember(dest=>dest.CustomerICountId,opt=>opt.MapFrom(src=>src.CustomerTbl.IcountId))
                .ForMember(dest => dest.IssueDescription, opt => opt.MapFrom(src => src.DeviceTbl.IssueDescription));

            CreateMap<Status, StatusDto>();
        }

        private int GetDeviceTypeId(string deviceType)
        {
            return deviceType.ToLower() switch
            {
                "מחשב" => 1,
                "טלפון" => 2,
                "אחר" => 3,
                _ => throw new ArgumentException("סוג מכשיר לא תקין")
            };
        }
    }

}
