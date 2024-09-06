using GatherUp.Domain.Entities;
using GatherUp.Domain.Helpers;
using GatherUp.Domain.Models.HelperModels;

namespace GatherUp.Domain.Repositories.UserRepositories;

public interface IUserQueryRepository
{
    PaginationHelper<User> GetAll(PaginationRequest request);
    Task<User> GetById(int id);
}
