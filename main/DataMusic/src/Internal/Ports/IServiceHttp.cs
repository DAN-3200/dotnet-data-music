namespace DataMusic.Internal.Ports;

public interface IServiceHttp
{
    Task<T?> RequestExternalApi<T>(string url) where T : class;
}