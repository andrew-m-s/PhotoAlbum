namespace PhotoAlbum.Wrappers;

public interface IConsoleWrapper
{
    string ReadLine();
    void Write(string content);
    void WriteLine(string content);
}

public class ConsoleWrapper : IConsoleWrapper
{
    public string ReadLine()
    {
        return Console.ReadLine();
    }
    public void Write(string content) 
    {
        Console.Write(content);
    }

    public void WriteLine(string content)
    {
        Console.WriteLine(content);
    }
}