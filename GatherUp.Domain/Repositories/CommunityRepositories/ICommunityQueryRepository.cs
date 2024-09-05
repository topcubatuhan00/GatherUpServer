using GatherUp.Domain.Entities;
using GatherUp.Domain.Helpers;
using GatherUp.Domain.Models.HelperModels;

namespace GatherUp.Domain.Repositories.CommunityRepositories;

public interface ICommunityQueryRepository
{
    PaginationHelper<Community> GetAll(PaginationRequest request);
    Task<Community> GetById(int id);
}
