using PhotoAlbum.Models;
using PhotoAlbum.Wrappers;

public interface IPhotoAlbumService
{
    List<Photos> GetPhotos(int albumId);
}

public class PhotoAlbumService : IPhotoAlbumService
{
    private IApiClient _apiClient;

    public PhotoAlbumService(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public List<Photos> GetPhotos(int albumId)
    {
        throw new NotImplementedException();
    }
}