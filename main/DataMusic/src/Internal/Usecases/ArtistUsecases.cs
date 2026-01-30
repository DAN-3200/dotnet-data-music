using DataMusic.Internal.Dtos;
using DataMusic.Internal.Entities;
using DataMusic.Internal.Ports;

namespace DataMusic.Internal.Usecases;

public class ArtistUsecase(
  IPortsGenericRepo<Artist> repoArtist,
  IServiceHttp service
)
{
  public async Task SaveArtist(string name)
  {
    if (await repoArtist.GetByName(name) is not null)
      throw new ArgumentException("Esse Artista já está presente no sistema");

    var urlQuery = $"https://itunes.apple.com/search?term={name}&media=music&entity=musicArtist&limit=1";
    var result = await service.RequestExternalApi<MapArtistJson>(urlQuery);

    if (result is null)
      throw new ArgumentException("Não há Artista com tal nome");

    var artist = new Artist(result.ArtistId, result.ArtistName, result.PrimaryGenreName, result.ArtistLinkUrl);
    await repoArtist.Save(artist);
  }

  public async Task<Artist> GetArtist(QueryArtist info)
  {
    var artist = await repoArtist.GetByFilter(info);
    if (artist is null) throw new ArgumentException("Não foi possível achar Artista(s) com tal consulta");
    return artist;
  }

  public async Task EditArtist(string id, EditArtist info)
  {
    var artist = await repoArtist.GetById(id);
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

    await repoArtist.Edit(id, artist);
  }

  public async Task DeleteArtist(string name)
  {
    await repoArtist.DeleteByName(name);
  }
}