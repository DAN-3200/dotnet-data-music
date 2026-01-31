using DataMusic.Internal.Dtos;
using DataMusic.Internal.Usecases;
using Microsoft.AspNetCore.Mvc;

namespace DataMusic.External.Controllers;

[ApiController]
public class MusicController(MusicUsecase usecase) : ControllerBase
{
    [HttpPost("music/save")]
    public async Task<IActionResult> SaveMusic(string name)
    {
        await usecase.SaveMusic(name);
        return Created();
    }

    [HttpGet("music/get")]
    public async Task<IActionResult> GetMusic([FromQuery] QueryMusic query)
    {
        var response = await usecase.GetMusic(query);
        return Ok(response);
    }

    [HttpPatch("music/edit/{id}")]
    public async Task<IActionResult> EditMusic(string id, [FromBody] EditMusic info)
    {
        await usecase.EditMusic(id, info);
        return NoContent();
    }

    [HttpDelete("music/delete/{id}")]
    public async Task<IActionResult> DeleteMusic(string id)
    {
        await usecase.DeleteMusic(id);
        return NoContent();
    }
}