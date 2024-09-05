using AutoMapper;
using GatherUp.Application.Services;
using GatherUp.Domain.Dtos;
using GatherUp.Domain.Entities;
using GatherUp.Domain.Helpers;
using GatherUp.Domain.Models.EventUserRelationModels;
using GatherUp.Domain.Models.HelperModels;
using GatherUp.Domain.UnitOfWork;

namespace GatherUp.Persistance.Services;

public class EventUserRelationService : IEventUserRelationService
{
    #region Fields
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Ctor
    public EventUserRelationService
    (
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    #endregion
    public async Task Create(CreateEventUserRelationModel model)
    {
        using (var context = _unitOfWork.Create())
        {
            var entity = _mapper.Map<EventUserRelation>(model);
            await context.Repositories.eventUserRelationCommandRepository.AddAsync(entity);
            context.SaveChanges();
        }
    }

    public async Task<ResponseDto<PaginationHelper<EventUserRelation>>> GetAll(PaginationRequest request)
    {
        using (var context = _unitOfWork.Create())
        {
            var result = context.Repositories.eventUserRelationQueryRepository.GetAll(request);
            var paginationHelper = new PaginationHelper<EventUserRelation>(result.TotalCount, request.PageSize, request.PageNumber, null);
            var items = result.Items.Select(item => _mapper.Map<EventUserRelation>(item)).ToList();
            paginationHelper.Items = items;
            return ResponseDto<PaginationHelper<EventUserRelation>>.Success(paginationHelper, 200);
        }
    }

    public async Task<ResponseDto<EventUserRelation>> GetById(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            var result = await context.Repositories.eventUserRelationQueryRepository.GetById(id);
            return ResponseDto<EventUserRelation>.Success(result, 200);
        }
    }

    public async Task Remove(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            var check = await context.Repositories.eventUserRelationQueryRepository.GetById(id);
            if (check == null) throw new Exception("Not Found");

            await context.Repositories.eventUserRelationCommandRepository.RemoveById(id);
            context.SaveChanges();
        }
    }

    public async Task Update(UpdateEventUserRelationModel model)
    {
        using (var context = _unitOfWork.Create())
        {
            var check = await context.Repositories.eventUserRelationQueryRepository.GetById(model.Id);
            if (check == null) throw new Exception("Not Found");

            var entity = _mapper.Map<EventUserRelation>(model);
            await context.Repositories.eventUserRelationCommandRepository.UpdateAsync(entity);
            context.SaveChanges();
        }
    }
}
