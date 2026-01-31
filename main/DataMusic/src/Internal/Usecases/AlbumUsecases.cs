using DataMusic.Internal.Dtos;
using DataMusic.Internal.Entities;
using DataMusic.Internal.Ports;

namespace DataMusic.Internal.Usecases;

public class AlbumUsecase(
    IAlbumRepo repoAlbum,
    IArtistRepo repoArtist,
    IServiceHttp service
)
{
    public async Task SaveAlbum(string name)
    {
        if (await repoAlbum.GetByName(name) is not null)
            throw new ArgumentException("Esse Album já está presente no sistema");

        var url = $"https://itunes.apple.com/search?term={name}&media=music&entity=album&limit=1";
        var response = await service.RequestExternalApi<ItunesResponse<MapAlbumJson>>(url);
        var result = response!.Results[0];

        if (result is null)
            throw new ArgumentException("Não há Album com tal nome");

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
            artistId = await repoArtist.Save(newArtist);
        }

        var album = new Album(
            result.CollectionId,
            result.CollectionName,
            artistId!
        );

        await repoAlbum.Save(album);
    }

    public async Task<List<Album>> GetAlbum(QueryAlbum info)
    {
        var album = await repoAlbum.GetByFilter(info);
        if (album is null) throw new ArgumentException("Não foi possível achar Album(s) com tal consulta");
        return album;
    }

    public async Task EditAlbum(string id, EditAlbum info)
    {
        var album = await repoAlbum.GetById(id);
        if (album is null) throw new ArgumentException("Não há Album com tal nome");

        if (info.Name is not null)
        {
            album.SetName(info.Name);
        }

        if (info.Genrer is not null)
        {
            album.SetGenrer(info.Genrer);
        }

        if (info.ViewUrl is not null)
        {
            album.SetViewUrl(info.ViewUrl);
        }

        await repoAlbum.Edit(id, album);
    }

    public async Task DeleteAlbum(string name)
    {
        await repoAlbum.DeleteByName(name);
    }
}