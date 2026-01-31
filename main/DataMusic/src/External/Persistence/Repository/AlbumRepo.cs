using DataMusic.External.Persistence.Documents;
using DataMusic.Internal.Dtos;
using DataMusic.Internal.Ports;
using DataMusic.Internal.Entities;
using MongoDB.Driver;

namespace DataMusic.External.Persistence.Repository;

public class AlbumRepo(IMongoDatabase db) : IAlbumRepo
{
    private readonly IMongoCollection<AlbumDoc> _collectionAlbum = db.GetCollection<AlbumDoc>("album");
    private readonly IMongoCollection<ArtistDoc> _collectionArtist = db.GetCollection<ArtistDoc>("artist");


    public async Task<string> Save(Album info)
    {
        var document = AlbumDoc.ToDocument(info);
        await _collectionAlbum.InsertOneAsync(document);
        return document.Id;
    }

    public async Task<Album?> GetById(string id)
    {
        var res = await _collectionAlbum.Find(i => i.Id == id).FirstOrDefaultAsync();
        return res is null ? null : AlbumDoc.ToEntity(res);
    }

    public async Task<List<Album>?> GetByFilter(QueryAlbum info)
    {
        var builder = Builders<AlbumDoc>.Filter;
        var filter = builder.Empty;

        if (info.Name is not null) filter &= builder.Eq(u => u.Name.ToLower() , info.Name.ToLower());

        if (info.Genrer is not null) filter &= builder.Eq(u => u.Genrer, info.Genrer);

        if (!string.IsNullOrWhiteSpace(info.ArtistName))
        {
            var artistId = await _collectionArtist.Find(i => i.Name == info.ArtistName).Project(i => i.Id).FirstOrDefaultAsync();
            filter &= builder.Eq(i => i.FkArtists, artistId);
        }

        var res = await _collectionAlbum.Find(filter).ToListAsync();

        return res?.Select(i => AlbumDoc.ToEntity(i)).ToList() ?? [];
    }

    public async Task<Album?> GetByName(string name)
    {
        var res = await _collectionAlbum.Find(i => i.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        return res is null ? null : AlbumDoc.ToEntity(res);
    }

    public async Task Edit(string id, Album info)
    {
        await _collectionAlbum.ReplaceOneAsync(i => i.Id == id, AlbumDoc.ToDocument(info));
    }

    public async Task DeleteByName(string name)
    {
        await _collectionAlbum.DeleteOneAsync(i => i.Name.ToLower()  == name.ToLower() );
    }

    public async Task DeleteById(string id)
    {
        await _collectionAlbum.DeleteOneAsync(i => i.Id == id);
    }
}