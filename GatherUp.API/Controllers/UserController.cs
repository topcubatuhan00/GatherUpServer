using GatherUp.Application.Services;
using GatherUp.Domain.Models.HelperModels;
using GatherUp.Domain.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GatherUp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class UserController : ControllerBase
{
    #region Fields
    private readonly IUserService _userService;
    #endregion

    #region Ctor
    public UserController
    (
        IUserService userService
    )
    {
        _userService = userService;
    }
    #endregion

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveUser(int id)
    {
        await _userService.Remove(id);
        return Ok("User Successfully Deleted!");
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request)
    {
        var result = await _userService.GetAll(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _userService.GetById(id);
        return Ok(result);
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> Update(UpdateUserModel model)
    {
        await _userService.Update(model);
        return Ok("User Successfully Updated!");
    }
}
