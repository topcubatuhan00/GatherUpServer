using AutoMapper;
using GatherUp.Application.Services;
using GatherUp.Domain.Dtos;
using GatherUp.Domain.Entities;
using GatherUp.Domain.Helpers;
using GatherUp.Domain.Models.HelperModels;
using GatherUp.Domain.Models.UserModels;
using GatherUp.Domain.UnitOfWork;

namespace GatherUp.Persistance.Services;

public class UserService : IUserService
{
    #region Fields
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Ctor
    public UserService
    (
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    #endregion
    public async Task<ResponseDto<PaginationHelper<User>>> GetAll(PaginationRequest request)
    {
        using (var context = _unitOfWork.Create())
        {
            var result = context.Repositories.userQueryRepository.GetAll(request);
            var paginationHelper = new PaginationHelper<User>(result.TotalCount, request.PageSize, request.PageNumber, null);
            var items = result.Items.Select(item => _mapper.Map<User>(item)).ToList();
            paginationHelper.Items = items;
            return ResponseDto<PaginationHelper<User>>.Success(paginationHelper, 200);
        }
    }

    public async Task<ResponseDto<User>> GetById(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            var result = await context.Repositories.userQueryRepository.GetById(id);
            return ResponseDto<User>.Success(result, 200);
        }
    }

    public async Task Remove(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            var check = await context.Repositories.userQueryRepository.GetById(id);
            if (check == null) throw new Exception("Not Found");

            await context.Repositories.userCommandRepository.RemoveById(id);
            context.SaveChanges();
        }
    }

    public async Task Update(UpdateUserModel model)
    {
        using (var context = _unitOfWork.Create())
        {
            var check = await context.Repositories.userQueryRepository.GetById(model.Id);
            if (check == null) throw new Exception("Not Found");

            var entity = _mapper.Map<User>(model);
            await context.Repositories.userCommandRepository.UpdateAsync(entity);
            context.SaveChanges();
        }
    }
}
