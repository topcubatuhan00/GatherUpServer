using GatherUp.Application.Services;
using GatherUp.Domain.Models.AuthModels;
using Microsoft.AspNetCore.Mvc;

namespace GatherUp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    #region Fields
    private readonly IAuthService _authService;
    #endregion

    #region Ctor
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    #endregion

    #region Methods
    [HttpPost("[action]")]
    public async Task<IActionResult> Login(AuthLoginModel model)
    {
        var result = await _authService.Login(model);
        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register(AuthRegisterModel user)
    {
        await _authService.Register(user);
        return Ok();
    }
    #endregion
}
