using DataMusic.Internal.Dtos;
using DataMusic.Internal.Entities;

namespace DataMusic.Internal.Ports;

public interface IRepository<T> where T : class
{
    Task<string> Save(T info);
    Task<T?> GetById(string id);
    Task<T?> GetByName(string name);
    Task Edit(string id, T info);
    Task DeleteById(string id);
    Task DeleteByName(string name);
}

public interface IMusicRepo : IRepository<Music>
{
    Task<List<Music>?> GetByFilter(QueryMusic info);
}

public interface IAlbumRepo : IRepository<Album>
{
    Task<List<Album>?> GetByFilter(QueryAlbum info);
}

public interface IArtistRepo : IRepository<Artist>
{
    Task<List<Artist>?> GetByFilter(QueryArtist info);
}