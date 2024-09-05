using GatherUp.Domain.Models.AuthModels;

namespace GatherUp.Domain.Repositories.AuthRepositories;

public interface IAuthCommandRepository
{
    Task<bool> AddAsync(AuthRegisterModel user);
}
