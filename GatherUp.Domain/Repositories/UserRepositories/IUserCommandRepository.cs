using GatherUp.Domain.Entities;

namespace GatherUp.Domain.Repositories.UserRepositories;

public interface IUserCommandRepository
{
    Task UpdateAsync(User entity);
    Task RemoveById(int id);
}
