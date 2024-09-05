using AutoMapper;
using GatherUp.Application.Services;
using GatherUp.Domain.Dtos;
using GatherUp.Domain.Entities;
using GatherUp.Domain.Helpers;
using GatherUp.Domain.Models.CommunityModels;
using GatherUp.Domain.Models.HelperModels;
using GatherUp.Domain.UnitOfWork;

namespace GatherUp.Persistance.Services;

public class CommunityService : ICommunityService
{
    #region Fields
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Ctor
    public CommunityService
    (
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    #endregion
    public async Task Create(CreateCommunityModel model)
    {
        using (var context = _unitOfWork.Create())
        {
            var entity = _mapper.Map<Community>(model);
            await context.Repositories.communityCommandRepository.AddAsync(entity);
            context.SaveChanges();
        }
    }

    public async Task<ResponseDto<PaginationHelper<Community>>> GetAll(PaginationRequest request)
    {
        using (var context = _unitOfWork.Create())
        {
            var result = context.Repositories.communityQueryRepository.GetAll(request);
            var paginationHelper = new PaginationHelper<Community>(result.TotalCount, request.PageSize, request.PageNumber, null);
            var items = result.Items.Select(item => _mapper.Map<Community>(item)).ToList();
            paginationHelper.Items = items;
            return ResponseDto<PaginationHelper<Community>>.Success(paginationHelper, 200);
        }
    }

    public async Task<ResponseDto<Community>> GetById(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            var result = await context.Repositories.communityQueryRepository.GetById(id);
            return ResponseDto<Community>.Success(result, 200);
        }
    }

    public async Task Remove(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            var check = await context.Repositories.communityQueryRepository.GetById(id);
            if (check == null) throw new Exception("Not Found");

            await context.Repositories.communityCommandRepository.RemoveById(id);
            context.SaveChanges();
        }
    }

    public async Task Update(UpdateCommunityModel model)
    {
        using (var context = _unitOfWork.Create())
        {
            var check = await context.Repositories.communityQueryRepository.GetById(model.Id);
            if (check == null) throw new Exception("Not Found");

            var entity = _mapper.Map<Community>(model);
            await context.Repositories.communityCommandRepository.UpdateAsync(entity);
            context.SaveChanges();
        }
    }
}
