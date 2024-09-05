using GatherUp.Domain.Entities;
using GatherUp.Domain.Helpers;
using GatherUp.Domain.Models.HelperModels;
namespace GatherUp.Domain.Repositories.EventRepositories;

public interface IEventQueryRepository
{
    PaginationHelper<Event> GetAll(PaginationRequest request);
    Task<Event> GetById(int id);
}
