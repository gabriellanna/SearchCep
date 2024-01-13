using AutoMapper;
using Cep.Domain.Dtos.Address;
using Cep.Domain.Models.Gateways.BrasilApi;

namespace Cep.Service.Configurations
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AddressResponseDto, ResponseApi>().ReverseMap();
        }
    }
}