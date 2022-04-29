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
        return await _apiClient.GetAsync<List<Photos>>("https://jsonplaceholder.typicode.com/photos");
    }
}