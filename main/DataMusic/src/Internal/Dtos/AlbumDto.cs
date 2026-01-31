namespace DataMusic.Internal.Dtos;

public record MapAlbumJson(
    int ArtistId,
    int CollectionId,
    string ArtistName,
    string CollectionName,
    string PrimaryGenreName,
    string ArtistViewUrl,
    string CollectionViewUrl
);

public record QueryAlbum(
    string? Name,
    string? Genrer,
    string? ArtistName
);

public record EditAlbum(
    string? Name,
    string? Genrer,
    string? ViewUrl
);