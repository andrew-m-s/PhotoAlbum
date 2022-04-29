namespace PhotoAlbum.Wrappers;

public interface IConsoleWrapper
{
    void WriteLine(string content);
}

public class ConsoleWrapper : IConsoleWrapper
{
    public void WriteLine(string content)
    {
        Console.WriteLine(content);
    }
}