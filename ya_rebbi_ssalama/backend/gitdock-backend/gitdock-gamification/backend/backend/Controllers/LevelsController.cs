using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/levels")]
public class LevelsController : ControllerBase
{
    private readonly ILevelService _levelService;

    public LevelsController(ILevelService levelService)
    {
        _levelService = levelService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _levelService.GetAllWithRequirementsAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLevelDto dto)
    {
        var result = await _levelService.CreateLevelAsync(dto);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] CreateLevelDto dto)
    {
        if (!Guid.TryParse(id, out var guidId))
        {
            return BadRequest("L'ID n'est pas un GUID valide.");
        }

        var success = await _levelService.UpdateLevelAsync(guidId, dto);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _levelService.DeleteLevelAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
