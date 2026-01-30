namespace DataMusic.Internal.Dtos;

public record MapMusicJson(
  int ArtistId,
  int CollectionId,
  int TrackId,
  string ArtistName,
  string CollectionName,
  string TrackName,
  string ArtistViewUrl,
  string PrimaryGenreName
);

public record QueryMusic(
  string? Name,
  string? Genrer,
  string? ArtistName,
  string? AlbumName
);

public record EditMusic(
  string? Name,
  string? Genrer,
  string? ViewUrl
);