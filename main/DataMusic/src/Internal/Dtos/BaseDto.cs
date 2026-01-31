namespace DataMusic.Internal.Dtos;

public record ItunesResponse<T>(
    int ResultCount,
    List<T> Results
);