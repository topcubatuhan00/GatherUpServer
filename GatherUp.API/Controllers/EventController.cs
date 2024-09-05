using GatherUp.Application.Services;
using GatherUp.Domain.Models.EventModels;
using GatherUp.Domain.Models.HelperModels;
using Microsoft.AspNetCore.Mvc;

namespace GatherUp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    #region Fields
    private readonly IEventService _eventService;
    #endregion

    #region Ctor
    public EventController
    (
        IEventService eventService
    )
    {
        _eventService = eventService;
    }
    #endregion

    #region Methods
    [HttpPost("[action]")]
    public async Task<IActionResult> Create(CreateEventModel model)
    {
        await _eventService.Create(model);
        return Ok("Event Successfully Added!");
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> Update(UpdateEventModel model)
    {
        await _eventService.Update(model);
        return Ok("Event Successfully Updated!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        await _eventService.Remove(id);
        return Ok("Event Successfully Deleted!");
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request)
    {
        var result = await _eventService.GetAll(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _eventService.GetById(id);
        return Ok(result);
    }
    #endregion
}
