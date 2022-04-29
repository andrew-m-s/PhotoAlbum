using PhotoAlbum.Wrappers;

namespace PhotoAlbum.Services;

public interface IConsoleService 
{
    void StartApplication();
}

public class ConsoleService : IConsoleService
{
    private IConsoleWrapper _consoleWrapper;

    public ConsoleService(IConsoleWrapper consoleWrapper) 
    {
        _consoleWrapper = consoleWrapper;
    }

    public void StartApplication() {
        _consoleWrapper.WriteLine("Hello, World!");
    }
}