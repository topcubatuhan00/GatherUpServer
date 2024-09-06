using AutoMapper;
using GatherUp.Domain.Entities;
using GatherUp.Domain.Models.CommunityModels;
using GatherUp.Domain.Models.EventModels;
using GatherUp.Domain.Models.EventUserRelationModels;
using GatherUp.Domain.Models.UserModels;

namespace GatherUp.Persistance.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateCommunityModel, Community>().ReverseMap();
        CreateMap<UpdateCommunityModel, Community>().ReverseMap();
        CreateMap<CreateEventModel, Event>().ReverseMap();
        CreateMap<UpdateEventModel, Event>().ReverseMap();
        CreateMap<CreateEventUserRelationModel, EventUserRelation>().ReverseMap();
        CreateMap<UpdateEventUserRelationModel, EventUserRelation>().ReverseMap();
        CreateMap<UpdateUserModel, User>().ReverseMap();
    }
}
