using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TagResponseDto>>> GetAll()
    {
        var result = await _tagService.GetAllTagsAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTagDto dto)
    {
        var tag = await _tagService.CreateTagAsync(dto);
        return CreatedAtAction(nameof(GetAll), new { id = tag.Id }, tag);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TagResponseDto>> Update(Guid id, CreateTagDto dto)
    {
        var result = await _tagService.UpdateTagAsync(id, dto);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _tagService.DeleteTagAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
