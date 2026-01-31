using DataMusic.Internal.Dtos;
using DataMusic.Internal.Entities;
using DataMusic.Internal.Usecases;
using Microsoft.AspNetCore.Mvc;

namespace DataMusic.External.Controllers;

[ApiController]
public class AlbumController(AlbumUsecase usecase) : ControllerBase
{
    [HttpPost("album/save")]
    public async Task<IActionResult> SaveAlbum(string name)
    {
        await usecase.SaveAlbum(name);
        return Created();
    }

    [HttpGet("album/get")]
    public async Task<IActionResult> GetAlbum([FromQuery] QueryAlbum query)
    {
        var response = await usecase.GetAlbum(query);
        return Ok(response);
    }

    [HttpPatch("album/edit/{id}")]
    public async Task<IActionResult> EditAlbum(string id, [FromBody] EditAlbum info)
    {
        await usecase.EditAlbum(id, info);
        return NoContent();
    }

    [HttpDelete("album/delete/{id}")]
    public async Task<IActionResult> DeleteAlbum(string id)
    {
        await usecase.DeleteAlbum(id);
        return NoContent();
    }
}