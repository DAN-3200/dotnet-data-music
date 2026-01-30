using DataMusic.Internal.Dtos;
using DataMusic.Internal.Entities;
using DataMusic.Internal.Ports;

namespace DataMusic.Internal.Usecases;

public class ArtistUsecase(IPortsGenericRepo<Artist> repo, IServiceHttp service)
{
  public async Task SaveArtist(string name)
  {
    // formar query
    var url = $"";
    // tratar erro
    var result = await service.RequestExternalApi<MapArtistJson>(url);
    var artist = new Artist(result!.ArtistId, result.ArtistName, result.PrimaryGenreName, result.ArtistLinkUrl);
    await repo.Save(artist);
  }

  public async Task<Artist> GetArtist(string name)
  {
    var artist = await repo.GetByName(name);
    if (artist is null) throw new ArgumentException("Não há Artista com tal nome");
    return artist;
  }

  public async Task EditArtist(string id, EditArtist info)
  {
    var artist = await repo.GetById(id);
    if (artist is null) throw new ArgumentException("Não há Artista com tal nome");

    if (info.Name is not null)
    {
      artist.SetName(info.Name);
    }

    if (info.Genrer is not null)
    {
      artist.SetGenrer(info.Genrer);
    }

    if (info.ViewUrl is not null)
    {
      artist.SetViewUrl(info.ViewUrl);
    }

    await repo.Edit(id, artist);
  }

  public async Task DeleteArtist(string name)
  {
    await repo.DeleteByName(name);
  }
}