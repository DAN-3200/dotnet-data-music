using DataMusic.Internal.Dtos;
using DataMusic.Internal.Usecases;
using Microsoft.AspNetCore.Mvc;

namespace DataMusic.External.Controllers;

[ApiController]
public class ArtistController(ArtistUsecase usecase) : ControllerBase
{
    [HttpPost("artist/save")]
    public async Task<IActionResult> SaveArtist(string name)
    {
        await usecase.SaveArtist(name);
        return Created();
    }

    [HttpGet("artist/get")]
    public async Task<IActionResult> GetArtist([FromQuery] QueryArtist query)
    {
        var response = await usecase.GetArtist(query);
        return Ok(response);
    }

    [HttpPatch("artist/edit/{id}")]
    public async Task<IActionResult> EditArtist(string id, [FromBody] EditArtist info)
    {
        await usecase.EditArtist(id, info);
        return NoContent();
    }

    [HttpDelete("artist/delete/{id}")]
    public async Task<IActionResult> DeleteArtist(string id)
    {
        await usecase.DeleteArtist(id);
        return NoContent();
    }
}