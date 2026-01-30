namespace DataMusic.Internal.Entities;

public class Album(
  string? id,
  int originalId,
  string name,
  string? genrer,
  string? viewUrl,
  string fkArtists,
  DateTime? createdAt
)
{
  public string? Id { get; private set; } = id;
  public int OriginalId { get; private set; } = originalId;
  public string Name { get; private set; } = name;
  public string? Genrer { get; private set; } = genrer;
  public string? ViewUrl { get; private set; } = viewUrl;
  public string FkArtists { get; private set; } = fkArtists;
  public DateTime? CreatedAt { get; private set; } = createdAt ?? DateTime.UtcNow;


  public Album(int originalId, string name, string? genrer, string? viewUrl, string fkArtists) :
    this(id: null, originalId, name, genrer, viewUrl, fkArtists: fkArtists, createdAt: null)
  {
  }

  public Album(int originalId, string name, string fkArtists) :
    this(id: null, originalId, name, null, null, fkArtists: fkArtists, null)
  {
  }

  public void SetName(string name)
  {
    if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name invalido");
    this.Name = name;
  }

  public void SetGenrer(string genrer)
  {
    if (string.IsNullOrEmpty(genrer)) throw new ArgumentException("genrer invalido");
    this.Genrer = genrer;
  }

  public void SetViewUrl(string viewUrl)
  {
    if (string.IsNullOrEmpty(viewUrl)) throw new ArgumentException("viewUrl invalido");
    this.ViewUrl = viewUrl;
  }
}
