using GatherUp.Domain.Entities;

namespace GatherUp.Domain.Repositories.AuthRepositories;

public interface IAuthQueryRepository
{
    Task<User> GetByUserName(string userName);
}
