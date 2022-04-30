using PhotoAlbum.Models;
using PhotoAlbum.Wrappers;

public interface IPhotoAlbumService
{
    Task<List<Photos>> GetPhotos(int albumId);
}

public class PhotoAlbumService : IPhotoAlbumService
{
    private IApiClient _apiClient;

    public PhotoAlbumService(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<List<Photos>> GetPhotos(int albumId)
    {
        var uri = $"https://jsonplaceholder.typicode.com/photos?albumId={albumId}";
        
        return await _apiClient.GetAsync<List<Photos>>(uri);
    }
}