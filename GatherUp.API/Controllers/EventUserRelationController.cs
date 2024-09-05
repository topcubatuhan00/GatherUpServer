using GatherUp.Application.Services;
using GatherUp.Domain.Models.EventUserRelationModels;
using GatherUp.Domain.Models.HelperModels;
using Microsoft.AspNetCore.Mvc;

namespace GatherUp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventUserRelationController : ControllerBase
{
    #region Fields
    private readonly IEventUserRelationService _eventUserService;
    #endregion

    #region Ctor
    public EventUserRelationController
    (
        IEventUserRelationService eventUserService
    )
    {
        _eventUserService = eventUserService;
    }
    #endregion

    #region Methods
    [HttpPost("[action]")]
    public async Task<IActionResult> Create(CreateEventUserRelationModel model)
    {
        await _eventUserService.Create(model);
        return Ok("Event Successfully Added!");
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> Update(UpdateEventUserRelationModel model)
    {
        await _eventUserService.Update(model);
        return Ok("Event Successfully Updated!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        await _eventUserService.Remove(id);
        return Ok("Event Successfully Deleted!");
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request)
    {
        var result = await _eventUserService.GetAll(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _eventUserService.GetById(id);
        return Ok(result);
    }
    #endregion
}
