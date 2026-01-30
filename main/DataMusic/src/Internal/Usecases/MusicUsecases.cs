using DataMusic.Internal.Dtos;
using DataMusic.Internal.Entities;
using DataMusic.Internal.Ports;

namespace DataMusic.Internal.Usecases;

public class MusicUsecase(
  IPortsGenericRepo<Music> repoMusic,
  IPortsGenericRepo<Album> repoAlbum,
  IPortsGenericRepo<Artist> repoArtist,
  IServiceHttp service
)
{
  public async Task SaveAlbum(string name)
  {
    if (await repoMusic.GetByName(name) is not null)
      throw new ArgumentException("Essa Musica já está presente no sistema");

    var url = $"https://itunes.apple.com/search?term={name}&media=music&entity=song&limit=1";
    var result = await service.RequestExternalApi<MapMusicJson>(url);

    if (result is null)
      throw new ArgumentException("Não há Musica com tal nome");

    var artist = await repoArtist.GetByName(result.ArtistName);

    string? artistId = artist?.Id;
    if (artist is null)
    {
      var newArtist = new Artist(
        result.ArtistId,
        result.ArtistName,
        result.PrimaryGenreName,
        result.ArtistViewUrl
      );
      await repoArtist.Save(newArtist);
      artistId = newArtist.Id;
    }

    var album = await repoAlbum.GetByName(result.CollectionName);

    string? albumId = album?.Id;
    if (album is null)
    {
      var newAlbum = new Album(
        result.CollectionId,
        result.CollectionName,
        fkArtists: artistId!
      );
      await repoAlbum.Save(newAlbum);
      albumId = newAlbum.Id;
    }

    var newMusic = new Music(
      result.TrackId,
      result.TrackName,
      result.PrimaryGenreName,
      result.ArtistViewUrl,
      fkArtists: artistId!,
      fkAlbum: albumId!
    );

    await repoMusic.Save(newMusic);
  }


  public async Task<Music> GetMusic(QueryMusic info)
  {
    var music = await repoMusic.GetByFilter(info);
    if (music is null) throw new ArgumentException("Não foi possível achar Musica(s) com tal consulta");
    return music;
  }

  public async Task EditMusic(string id, EditMusic info)
  {
    var music = await repoMusic.GetById(id);
    if (music is null) throw new ArgumentException("Não há Musica com tal nome");

    if (info.Name is not null)
    {
      music.SetName(info.Name);
    }

    if (info.Genrer is not null)
    {
      music.SetGenrer(info.Genrer);
    }

    if (info.ViewUrl is not null)
    {
      music.SetViewUrl(info.ViewUrl);
    }

    await repoMusic.Edit(id, music);
  }

  public async Task DeleteMusic(string name)
  {
    await repoMusic.DeleteByName(name);
  }
}