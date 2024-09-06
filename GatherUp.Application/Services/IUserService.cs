using GatherUp.Domain.Dtos;
using GatherUp.Domain.Entities;
using GatherUp.Domain.Helpers;
using GatherUp.Domain.Models.HelperModels;
using GatherUp.Domain.Models.UserModels;

namespace GatherUp.Application.Services;

public interface IUserService
{
    Task Update(UpdateUserModel model);
    Task Remove(int id);
    Task<ResponseDto<PaginationHelper<User>>> GetAll(PaginationRequest request);
    Task<ResponseDto<User>> GetById(int id);
}
