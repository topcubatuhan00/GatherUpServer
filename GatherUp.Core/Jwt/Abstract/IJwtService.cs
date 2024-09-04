using GatherUp.Domain.Entities;
using GatherUp.Domain.Models.HelperModels;

namespace GatherUp.Core.Jwt.Abstract;

public interface IJwtService
{
    TokenResponseModel CreateToken(User user);
}
