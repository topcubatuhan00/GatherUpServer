using GatherUp.Domain.Entities;

namespace GatherUp.Domain.Repositories.EventRepositories;

public interface IEventCommandRepository
{
    Task AddAsync(Event entity);
    Task UpdateAsync(Event entity);
    Task RemoveById(int id);
}
