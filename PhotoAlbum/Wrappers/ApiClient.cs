using System.Runtime.CompilerServices;
using System.Text.Json;

namespace PhotoAlbum.Wrappers;

public interface IApiClient
{
    Task<T> GetAsync<T>(string uri);
}

public class ApiClient : IApiClient
{
    public async Task<T> GetAsync<T>(string uri)
    {
        var client = new HttpClient();
        var response = await client.GetAsync(uri);
        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(content);
    }
}