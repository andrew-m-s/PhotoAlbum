using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoAlbum.Services;
using PhotoAlbum.Wrappers;

namespace PhotoAlbum.Tests;

[TestClass]
public class ConsoleServiceTests
{
    private ConsoleService _consoleService;
    private Mock<IConsoleWrapper> _mockConsoleWrapper;

    [TestInitialize]
    public void BeforeEach()
    {
        _mockConsoleWrapper = new Mock<IConsoleWrapper>();
        _consoleService = new ConsoleService(_mockConsoleWrapper.Object);
    }

    [TestMethod]
    public void ShouldCallWriteLineWithHelloWorldValue()
    {
        _consoleService.StartApplication();
        _mockConsoleWrapper.Verify(x => x.WriteLine("Hello, World!"), Times.Once);
    }
}