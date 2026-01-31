using DataMusic.Internal.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataMusic.External.Persistence.Documents;

public class AlbumDoc
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public int OriginalId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Genrer { get; private set; }
    public string? ViewUrl { get; private set; }
    public string FkArtists { get; private set; } = string.Empty;
    public DateTime? CreatedAt { get; private set; }

    public static AlbumDoc ToDocument(Album entity)
    {
        return new AlbumDoc
        {
            Id = entity.Id!,
            OriginalId = entity.OriginalId,
            Name = entity.Name,
            Genrer = entity.Genrer,
            ViewUrl = entity.ViewUrl,
            FkArtists = entity.FkArtists,
            CreatedAt = entity.CreatedAt
        };
    }

    public static Album ToEntity(AlbumDoc doc)
    {
        return new Album(
            id: doc.Id,
            originalId: doc.OriginalId,
            name: doc.Name,
            genrer: doc.Genrer,
            viewUrl: doc.ViewUrl,
            fkArtists: doc.FkArtists,
            createdAt: doc.CreatedAt
        );
    }
}