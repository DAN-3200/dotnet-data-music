using DataMusic.External.Persistence.Documents;
using DataMusic.Internal.Dtos;
using DataMusic.Internal.Ports;
using DataMusic.Internal.Entities;
using MongoDB.Driver;

namespace DataMusic.External.Persistence.Repository;

public class ArtistRepo(IMongoDatabase db) : IArtistRepo
{
    private readonly IMongoCollection<ArtistDoc> _collection = db.GetCollection<ArtistDoc>("artist");

    public async Task<string> Save(Artist info)
    {
        var document = ArtistDoc.ToDocument(info);
        await _collection.InsertOneAsync(document);
        return document.Id;
    }

    public async Task<Artist?> GetById(string id)
    {
        var res = await _collection.Find(i => i.Id == id).FirstOrDefaultAsync();
        return res is null ? null : ArtistDoc.ToEntity(res);
    }

    public async Task<List<Artist>?> GetByFilter(QueryArtist info)
    {
        var builder = Builders<ArtistDoc>.Filter;
        var filter = builder.Empty;

        if (info.Name is not null) filter &= builder.Eq(u => u.Name, info.Name);

        if (info.Genrer is not null) filter &= builder.Eq(u => u.Genrer, info.Genrer);

        var res = await _collection.Find(filter).ToListAsync();

        return res?.Select(i => ArtistDoc.ToEntity(i)).ToList() ?? [];
    }

    public async Task<Artist?> GetByName(string name)
    {
        var res = await _collection.Find(i => i.Name.ToLower()  == name.ToLower() ).FirstOrDefaultAsync();
        return res is null ? null : ArtistDoc.ToEntity(res);
    }

    public async Task Edit(string id, Artist info)
    {
        await _collection.ReplaceOneAsync(i => i.Id == id, ArtistDoc.ToDocument(info));
    }

    public async Task DeleteByName(string name)
    {
        await _collection.DeleteOneAsync(i => i.Name.ToLower()  == name.ToLower() );
    }

    public async Task DeleteById(string id)
    {
        await _collection.DeleteOneAsync(i => i.Id == id);
    }
}