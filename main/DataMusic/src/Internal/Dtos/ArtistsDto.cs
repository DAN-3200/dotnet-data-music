namespace DataMusic.Internal.Dtos;

public record EditArtist(
  string? Name,
  string? Genrer,
  string? ViewUrl
);

public record MapArtistJson(
  int ArtistId,
  string ArtistName,
  string ArtistLinkUrl,
  string PrimaryGenreName
);