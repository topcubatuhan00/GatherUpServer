using GatherUp.Core.Jwt.Abstract;
using GatherUp.Domain.Entities;
using GatherUp.Domain.Models.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GatherUp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    #region Fields
    private readonly IJwtService _jwtService;
    #endregion

    #region Ctor
    public AuthController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }
    #endregion

    #region Methods
    [HttpGet]
    public TokenResponseModel Login(string userName, string role, string password)
    {
        User user = new User
        {
            UserName = userName,
            Role = role,
            Password = password,
            Name = "Batuhan",
            LastName = "Topcu",
            Email = "a@a.com",
            CreatedDate = DateTime.Now,
            CreatorName = "Batuhan",
            DeletedTime = null,
            DeleterName = null,
            Id = 1,
            IsActive = true,
            IsConfirmed = true,
            Picture = "",
            UpdatedDate = null,
            UpdaterName = null
        };

        var response = _jwtService.CreateToken(user);
        return response;
    }
    #endregion
}
