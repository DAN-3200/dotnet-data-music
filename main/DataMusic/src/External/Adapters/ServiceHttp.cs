using DataMusic.Internal.Ports;

namespace DataMusic.External.Adapters;

public class ServiceHttp(HttpClient client) : IServiceHttp
{
    public async Task<T?> RequestExternalApi<T>(string url) where T : class
    {
        return await client.GetFromJsonAsync<T>(url);
    }
}