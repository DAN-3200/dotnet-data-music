namespace DataMusic.Internal.Entities;

public class Music
{
  public string? Id { get; private set; }
  public int OriginalId { get; private set; }
  public string Name { get; private set; }
  public string? Genrer { get; private set; }
  public string? ViewUrl { get; private set; }
  public DateTime CreatedAt { get; private set; }
  public string FkArtists { get; private set; }
  public string FkAlbum { get; private set; }
}
