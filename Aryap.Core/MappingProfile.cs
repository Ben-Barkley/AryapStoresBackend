using AutoMapper;
//using com.sun.xml.@internal.ws.api.policy;
using Data.Entities;
using Data.Models;

namespace Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map between database entities and DTOs
            CreateMap<SourceEntity, SourceModel>().ReverseMap();
            CreateMap<DestinationEntity, DestinationModel>().ReverseMap();
        }
    }
}