using GatherUp.Domain.Entities;
using GatherUp.Domain.Helpers;
using GatherUp.Domain.Models.HelperModels;

namespace GatherUp.Domain.Repositories.EventUserRelationRepositories;

public interface IEventUserRelationQueryRepository
{
    PaginationHelper<EventUserRelation> GetAll(PaginationRequest request);
    Task<EventUserRelation> GetById(int id);
}
