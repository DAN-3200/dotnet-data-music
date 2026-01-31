using DataMusic.External.Adapters;
using DataMusic.Internal.Dtos;
using Xunit.Abstractions;

namespace DataMusic.Tests;

public class TestUnitario (ITestOutputHelper output)
{
    [Fact]
    public async Task TestRequestExternalApi()
    {
        var client = new HttpClient();
        var x = new ServiceHttp(client);

        var response =
            await x.RequestExternalApi<ItunesResponse<MapMusicJson>>(
                "https://itunes.apple.com/search?term=coisar&media=music&entity=song&limit=1");
        output.WriteLine(response?.Results[0].ArtistName ?? "NULL");
        
    }
}
