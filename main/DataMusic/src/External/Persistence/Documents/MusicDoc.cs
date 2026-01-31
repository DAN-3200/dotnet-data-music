using DataMusic.Internal.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataMusic.External.Persistence.Documents;

public class MusicDoc
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public int OriginalId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Genrer { get; private set; }
    public string? ViewUrl { get; private set; }
    public string FkArtists { get; private set; } = string.Empty;
    public string FkAlbum { get; private set; } = string.Empty;
    public DateTime? CreatedAt { get; private set; }

    public static MusicDoc ToDocument(Music entity)
    {
        return new MusicDoc
        {
            Id = entity.Id!,
            OriginalId = entity.OriginalId,
            Name = entity.Name,
            Genrer = entity.Genrer,
            ViewUrl = entity.ViewUrl,
            FkArtists = entity.FkArtists,
            FkAlbum = entity.FkAlbum,
            CreatedAt = entity.CreatedAt
        };
    }

    public static Music ToEntity(MusicDoc doc)
    {
        return new Music(
            id: doc.Id,
            originalId: doc.OriginalId,
            name: doc.Name,
            genrer: doc.Genrer,
            viewUrl: doc.ViewUrl,
            fkArtists: doc.FkArtists,
            fkAlbum: doc.FkAlbum,
            createdAt: doc.CreatedAt
        );
    }
}