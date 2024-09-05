using GatherUp.Domain.Dtos;
using GatherUp.Domain.Entities;
using GatherUp.Domain.Helpers;
using GatherUp.Domain.Models.EventModels;
using GatherUp.Domain.Models.HelperModels;

namespace GatherUp.Application.Services;

public interface IEventService
{
    Task<ResponseDto<Event>> GetById(int id);
    Task<ResponseDto<PaginationHelper<Event>>> GetAll(PaginationRequest request);
    Task Create(CreateEventModel model);
    Task Update(UpdateEventModel model);
    Task Remove(int id);
}
