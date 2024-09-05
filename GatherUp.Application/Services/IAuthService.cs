using GatherUp.Domain.Models.AuthModels;
using GatherUp.Domain.Models.HelperModels;

namespace GatherUp.Application.Services;

public interface IAuthService
{
    Task<TokenResponseModel> Login(AuthLoginModel model);
    Task Register(AuthRegisterModel user);
}
