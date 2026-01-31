namespace DataMusic.External.Persistence;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DataBaseName { get; set; } = null!;
}