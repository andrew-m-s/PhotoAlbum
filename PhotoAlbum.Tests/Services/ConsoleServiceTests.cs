using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoAlbum.Models;
using PhotoAlbum.Services;
using PhotoAlbum.Wrappers;
using PhotoAlbum.Tests.Helpers;

namespace PhotoAlbum.Tests;

[TestClass]
public class ConsoleServiceTests
{
    private ConsoleService _consoleService;
    private Mock<IConsoleWrapper> _mockConsoleWrapper;
    private Mock<IPhotoAlbumService> _mockPhotoAlbumService;
    private List<Photos> expectedPhotos;

    [TestInitialize]
    public void BeforeEach()
    {
        expectedPhotos = PhotoHelpers.GetTestPhotoList();

        _mockConsoleWrapper = new Mock<IConsoleWrapper>();
        _mockConsoleWrapper.Setup(x => x.ReadLine())
            .Returns("1");

        _mockPhotoAlbumService = new Mock<IPhotoAlbumService>();
        _mockPhotoAlbumService
            .Setup(x => x.GetPhotos(It.IsAny<int>()))
            .Returns(Task.FromResult(expectedPhotos));

        _consoleService = new ConsoleService(_mockConsoleWrapper.Object, _mockPhotoAlbumService.Object);

    }

    [TestMethod]
    public void ShouldDisplayUserPromptOnStartup()
    {
        _consoleService.StartApplication();

        _mockConsoleWrapper.Verify(x => x.Write("Please enter an albumId: "));
    }

    [TestMethod]
    public void ShouldAcceptInputFromUser()
    {
        _consoleService.StartApplication();

        _mockConsoleWrapper.Verify(x=> x.ReadLine());
    }

    [TestMethod]
    public void ShouldCallGetPhotosWithUserInput()
    {
        var expectedUserInput = "2";
        _consoleService.StartApplication();
        
        _mockConsoleWrapper.Setup(x => x.ReadLine())
            .Returns(expectedUserInput);

        _mockPhotoAlbumService.Verify(x => x.GetPhotos(2), Times.Once);
    }

    [TestMethod]
    public void ShouldReturnAllPhotos()
    {
        _consoleService.StartApplication();

        expectedPhotos.ForEach(photo =>
        {
            _mockConsoleWrapper.Verify(x => x.WriteLine($"[{photo.Id}] {photo.Title}"), Times.Once);
        });
    }
}