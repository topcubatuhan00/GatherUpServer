using GatherUp.Domain.Entities;

namespace GatherUp.Domain.Repositories.EventUserRelationRepositories;

public interface IEventUserRelationCommandRepository
{
    Task AddAsync(EventUserRelation entity);
    Task UpdateAsync(EventUserRelation entity);
    Task RemoveById(int id);
}
