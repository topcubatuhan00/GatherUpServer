using AutoMapper;
using GatherUp.Application.Services;
using GatherUp.Domain.Dtos;
using GatherUp.Domain.Entities;
using GatherUp.Domain.Helpers;
using GatherUp.Domain.Models.EventModels;
using GatherUp.Domain.Models.HelperModels;
using GatherUp.Domain.UnitOfWork;

namespace GatherUp.Persistance.Services;

public class EventService : IEventService
{
    #region Fields
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Ctor
    public EventService
    (
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    #endregion
    public async Task Create(CreateEventModel model)
    {
        using (var context = _unitOfWork.Create())
        {
            var entity = _mapper.Map<Event>(model);
            await context.Repositories.eventCommandRepository.AddAsync(entity);
            context.SaveChanges();
        }
    }

    public async Task<ResponseDto<PaginationHelper<Event>>> GetAll(PaginationRequest request)
    {
        using (var context = _unitOfWork.Create())
        {
            var result = context.Repositories.eventQueryRepository.GetAll(request);
            var paginationHelper = new PaginationHelper<Event>(result.TotalCount, request.PageSize, request.PageNumber, null);
            var items = result.Items.Select(item => _mapper.Map<Event>(item)).ToList();
            paginationHelper.Items = items;
            return ResponseDto<PaginationHelper<Event>>.Success(paginationHelper, 200);
        }
    }

    public async Task<ResponseDto<Event>> GetById(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            var result = await context.Repositories.eventQueryRepository.GetById(id);
            return ResponseDto<Event>.Success(result, 200);
        }
    }

    public async Task Remove(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            var check = await context.Repositories.eventQueryRepository.GetById(id);
            if (check == null) throw new Exception("Not Found");

            await context.Repositories.eventCommandRepository.RemoveById(id);
            context.SaveChanges();
        }
    }

    public async Task Update(UpdateEventModel model)
    {
        using (var context = _unitOfWork.Create())
        {
            var check = await context.Repositories.eventQueryRepository.GetById(model.Id);
            if (check == null) throw new Exception("Not Found");

            var entity = _mapper.Map<Event>(model);
            await context.Repositories.eventCommandRepository.UpdateAsync(entity);
            context.SaveChanges();
        }
    }
}
