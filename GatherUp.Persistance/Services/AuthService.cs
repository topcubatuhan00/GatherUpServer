using GatherUp.Application.Services;
using GatherUp.Core.Jwt.Abstract;
using GatherUp.Domain.Entities;
using GatherUp.Domain.Models.AuthModels;
using GatherUp.Domain.Models.HelperModels;
using GatherUp.Domain.UnitOfWork;

namespace GatherUp.Persistance.Services;

public class AuthService : IAuthService
{
    #region Fields
    private readonly IJwtService _jwtService;
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Ctor
    public AuthService
    (
        IUnitOfWork unitOfWork,
        IJwtService jwtService
    )
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }
    #endregion

    #region Methods
    public async Task<TokenResponseModel> Login(AuthLoginModel model)
    {
        var checkUser = await CheckUser(model.UserName);
        if (checkUser == null) throw new Exception("UserName Not Found");

        var checkPassword = VerifyPassword(model.Password, checkUser.Password);
        if (!checkPassword) throw new Exception("Password Not Correct");

        return _jwtService.CreateToken(checkUser);
    }

    public async Task Register(AuthRegisterModel user)
    {
        var checkUser = await CheckUser(user.UserName);
        if (checkUser != null) throw new Exception("UserName Already Taken");

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        using (var context = _unitOfWork.Create())
        {
            var result = await context.Repositories.authCommandRepository.AddAsync(user);
            if (!result) throw new Exception("Error In Register Operation");
            context.SaveChanges();
        }
    }
    #endregion
    #region Helpers
    private bool VerifyPassword(string pass, string hashedPassword)
    {
        if (!BCrypt.Net.BCrypt.Verify(pass, hashedPassword)) return false;
        return true;
    }

    private async Task<User> CheckUser(string userName)
    {
        using (var context = _unitOfWork.Create())
        {
            var result = await context.Repositories.authQueryRepository.GetByUserName(userName);
            return result;
        }
    }
    #endregion
}