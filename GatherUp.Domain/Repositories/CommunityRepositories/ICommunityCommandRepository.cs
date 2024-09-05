using GatherUp.Domain.Entities;

namespace GatherUp.Domain.Repositories.CommunityRepositories;

public interface ICommunityCommandRepository
{
    Task AddAsync(Community entity);
    Task UpdateAsync(Community entity);
    Task RemoveById(int id);
}
