namespace DataMusic.Internal.Entities;

public class Artist(
    string? id,
    int originalId,
    string name,
    string? genrer,
    string? viewUrl,
    DateTime? createdAt
)
{
    public string? Id { get; private set; } = id;
    public int OriginalId { get; private set; } = originalId;
    public string Name { get; private set; } = name.ToLower();
    public string? Genrer { get; private set; } = genrer?.ToLower();
    public string? ViewUrl { get; private set; } = viewUrl;
    public DateTime? CreatedAt { get; private set; } = createdAt ?? DateTime.UtcNow;

    public Artist(int originalId, string name, string? genrer, string? viewUrl) :
        this(id: null, originalId, name, genrer, viewUrl, createdAt: null)
    {
    }

    public Artist(int originalId, string name) :
        this(id: null, originalId, name, null, null, null)
    {
    }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name invalido");
        this.Name = name.ToLower();
    }

    public void SetGenrer(string genrer)
    {
        if (string.IsNullOrEmpty(genrer)) throw new ArgumentException("genrer invalido");
        this.Genrer = genrer.ToLower();
    }

    public void SetViewUrl(string viewUrl)
    {
        if (string.IsNullOrEmpty(viewUrl)) throw new ArgumentException("viewUrl invalido");
        this.ViewUrl = viewUrl;
    }
}