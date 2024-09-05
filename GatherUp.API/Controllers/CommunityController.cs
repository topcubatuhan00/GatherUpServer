using GatherUp.Application.Services;
using GatherUp.Domain.Models.CommunityModels;
using GatherUp.Domain.Models.HelperModels;
using Microsoft.AspNetCore.Mvc;

namespace GatherUp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommunityController : ControllerBase
{
    #region Fields
    private readonly ICommunityService _communityService;
    #endregion

    #region Ctor
    public CommunityController
    (
        ICommunityService communityService
    )
    {
        _communityService = communityService;
    }
    #endregion

    #region Methods
    [HttpPost("[action]")]
    public async Task<IActionResult> Create(CreateCommunityModel model)
    {
        await _communityService.Create(model);
        return Ok("Event Successfully Added!");
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> Update(UpdateCommunityModel model)
    {
        await _communityService.Update(model);
        return Ok("Event Successfully Updated!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        await _communityService.Remove(id);
        return Ok("Event Successfully Deleted!");
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request)
    {
        var result = await _communityService.GetAll(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _communityService.GetById(id);
        return Ok(result);
    }
    #endregion
}
