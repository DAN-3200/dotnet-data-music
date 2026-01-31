using DataMusic.External.Persistence.Documents;
using DataMusic.Internal.Dtos;
using DataMusic.Internal.Ports;
using DataMusic.Internal.Entities;
using MongoDB.Driver;

namespace DataMusic.External.Persistence.Repository;

public class MusicRepo(IMongoDatabase db) : IMusicRepo
{
    private readonly IMongoCollection<MusicDoc> _collectionMusic = db.GetCollection<MusicDoc>("music");
    private readonly IMongoCollection<AlbumDoc> _collectionAlbum = db.GetCollection<AlbumDoc>("album");
    private readonly IMongoCollection<ArtistDoc> _collectionArtist = db.GetCollection<ArtistDoc>("artist");


    public async Task<string> Save(Music info)
    {
        var document = MusicDoc.ToDocument(info);
        await _collectionMusic.InsertOneAsync(document);
        return document.Id;
    }

    public async Task<Music?> GetById(string id)
    {
        var res = await _collectionMusic.Find(i => i.Id == id).FirstOrDefaultAsync();
        return res is null ? null : MusicDoc.ToEntity(res);
    }

    public async Task<List<Music>?> GetByFilter(QueryMusic info)
    {
        var builder = Builders<MusicDoc>.Filter;
        var filter = builder.Empty;

        if (info.Name is not null) filter &= builder.Eq(u => u.Name, info.Name);

        if (info.Genrer is not null) filter &= builder.Eq(u => u.Genrer, info.Genrer);

        if (!string.IsNullOrWhiteSpace(info.AlbumName))
        {
            var albumId = await _collectionAlbum.Find(i => i.Name == info.AlbumName).Project(i => i.Id).FirstOrDefaultAsync();
            filter &= builder.Eq(i => i.FkAlbum, albumId);
        }

        if (!string.IsNullOrWhiteSpace(info.ArtistName))
        {
            var artistId = await _collectionArtist.Find(i => i.Name == info.ArtistName).Project(i => i.Id).FirstOrDefaultAsync();
            filter &= builder.Eq(i => i.FkArtists, artistId);
        }

        var res = await _collectionMusic.Find(filter).ToListAsync();
        return res?.Select(i => MusicDoc.ToEntity(i)).ToList() ?? [];
    }

    public async Task<Music?> GetByName(string name)
    {
        var res = await _collectionMusic.Find(i => i.Name == name).FirstOrDefaultAsync();
        return res is null ? null : MusicDoc.ToEntity(res);
    }

    public async Task Edit(string id, Music info)
    {
        await _collectionMusic.ReplaceOneAsync(i => i.Id == id, MusicDoc.ToDocument(info));
    }

    public async Task DeleteByName(string name)
    {
        await _collectionMusic.DeleteOneAsync(i => i.Name.ToLower()  == name.ToLower());
    }

    public async Task DeleteById(string id)
    {
        await _collectionMusic.DeleteOneAsync(i => i.Id == id);
    }
}