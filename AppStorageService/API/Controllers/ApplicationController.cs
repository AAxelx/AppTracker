using AppStorageService.Services.DTOs;
using AppStorageService.Services.Enums;
using AppStorageService.Services.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AppStorageService.API.Controllers;

[ApiController]
[Route("api/applications")]
public class ApplicationController(IApplicationService applicationService) : ControllerBase
{
  [HttpGet("status/{status}")]
  public async Task<IActionResult> GetByStatusAsync([FromRoute] ApplicationStatus status)
  {
    var applications = await applicationService.GetByStatusAsync(status);
    return Ok(applications);
  }

  [HttpPost]
  public async Task<IActionResult> AddAsync([FromBody] AddApplicationDto dto)
  {
    await applicationService.AddAsync(dto);
    return Created();
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
  {
    if (id == Guid.Empty)
    {
      return BadRequest("Invalid ID");
    }
    
    await applicationService.DeleteAsync(id);
    return NoContent();
  }
}