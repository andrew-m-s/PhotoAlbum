using PhotoAlbum.Wrappers;

namespace PhotoAlbum.Services;

public interface IConsoleService
{
    void StartApplication();
}

public class ConsoleService : IConsoleService
{
    private IConsoleWrapper _consoleWrapper;
    private IPhotoAlbumService _photoAlbumService;

    public ConsoleService(IConsoleWrapper consoleWrapper, IPhotoAlbumService photoAlbumService)
    {
        _consoleWrapper = consoleWrapper;
        _photoAlbumService = photoAlbumService;
    }

    public void DisplayPhotosByAlbumId(int albumId) {
        var photoResults = _photoAlbumService.GetPhotos(albumId).Result;

        _consoleWrapper.WriteLine($"photo-album {albumId}");

        photoResults.ForEach(x =>
        {
            _consoleWrapper.WriteLine($"[{x.Id}] {x.Title}");
        });
    }

    public void StartApplication()
    {
        _consoleWrapper.Write("Please enter an albumId: ");

        var userInput = _consoleWrapper.ReadLine();

        int albumIdInput;

        if (Int32.TryParse(userInput, out albumIdInput))
        {
            DisplayPhotosByAlbumId(albumIdInput);
        }
    }
}