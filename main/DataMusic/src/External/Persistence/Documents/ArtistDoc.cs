using DataMusic.Internal.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataMusic.External.Persistence.Documents;

public class ArtistDoc
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public int OriginalId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Genrer { get; private set; }
    public string? ViewUrl { get; private set; }
    public DateTime? CreatedAt { get; private set; }

    public static ArtistDoc ToDocument(Artist entity)
    {
        return new ArtistDoc
        {
            Id = entity.Id!,
            OriginalId = entity.OriginalId,
            Name = entity.Name,
            Genrer = entity.Genrer,
            ViewUrl = entity.ViewUrl,
            CreatedAt = entity.CreatedAt
        };
    }

    public static Artist ToEntity(ArtistDoc doc)
    {
        return new Artist(
            id: doc.Id,
            originalId: doc.OriginalId,
            name: doc.Name,
            genrer: doc.Genrer,
            viewUrl: doc.ViewUrl,
            createdAt: doc.CreatedAt
        );
    }
}