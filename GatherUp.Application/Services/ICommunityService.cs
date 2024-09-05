using GatherUp.Domain.Dtos;
using GatherUp.Domain.Entities;
using GatherUp.Domain.Helpers;
using GatherUp.Domain.Models.CommunityModels;
using GatherUp.Domain.Models.HelperModels;

namespace GatherUp.Application.Services;

public interface ICommunityService
{
    Task<ResponseDto<Community>> GetById(int id);
    Task<ResponseDto<PaginationHelper<Community>>> GetAll(PaginationRequest request);
    Task Create(CreateCommunityModel model);
    Task Update(UpdateCommunityModel model);
    Task Remove(int id);

}
