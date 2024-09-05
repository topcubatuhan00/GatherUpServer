using GatherUp.Domain.Dtos;
using GatherUp.Domain.Entities;
using GatherUp.Domain.Helpers;
using GatherUp.Domain.Models.EventUserRelationModels;
using GatherUp.Domain.Models.HelperModels;

namespace GatherUp.Application.Services;

public interface IEventUserRelationService
{
    Task<ResponseDto<EventUserRelation>> GetById(int id);
    Task<ResponseDto<PaginationHelper<EventUserRelation>>> GetAll(PaginationRequest request);
    Task Create(CreateEventUserRelationModel model);
    Task Update(UpdateEventUserRelationModel model);
    Task Remove(int id);
}
