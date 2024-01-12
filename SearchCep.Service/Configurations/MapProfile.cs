using AutoMapper;
using SearchCep.Domain.Dtos.Address;
using SearchCep.Domain.Models.Gateways.BrasilApi;

namespace SearchCep.Service.Configurations
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AddressResponseDto, ResponseApi>().ReverseMap();
        }
    }
}