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

    public void StartApplication()
    {
        _consoleWrapper.Write("Please enter an albumId: ");

        var userInput = _consoleWrapper.ReadLine();

        var photoResults = _photoAlbumService.GetPhotos(1).Result;

        photoResults.ForEach(x =>
        {
            _consoleWrapper.WriteLine($"[{x.Id}] {x.Title}");
        });
    }
}