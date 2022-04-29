using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoAlbum.Models;
using PhotoAlbum.Services;
using PhotoAlbum.Wrappers;
using PhotoAlbumTests.Helpers;

namespace PhotoAlbum.Tests;

[TestClass]
public class ConsoleServiceTests
{
    private ConsoleService _consoleService;
    private Mock<IConsoleWrapper> _mockConsoleWrapper;
    private Mock<IPhotoAlbumService> _mockPhotoAlbumService;
    private List<Photos> _expectedPhotos;

    [TestInitialize]
    public void BeforeEach()
    {
        _mockConsoleWrapper = new Mock<IConsoleWrapper>();

        _expectedPhotos = PhotoHelpers.GetTestPhotoList();
        
        _mockPhotoAlbumService = new Mock<IPhotoAlbumService>();
        _mockPhotoAlbumService
            .Setup(x => x.GetPhotos(It.IsAny<int>()))
            .Returns(Task.FromResult(_expectedPhotos));

        _consoleService = new ConsoleService(_mockConsoleWrapper.Object, _mockPhotoAlbumService.Object);

    }

    [TestMethod]
    public void ShouldCallGetPhotos()
    {
        _consoleService.StartApplication();

        _mockPhotoAlbumService.Verify(x => x.GetPhotos(It.IsAny<int>()), Times.Once);
    }

    [TestMethod]
    public void ShouldReturnAllPhotos()
    {
        _consoleService.StartApplication();

        _expectedPhotos.ForEach(photo =>
        {
            _mockConsoleWrapper.Verify(x => x.WriteLine($"[{photo.Id}] {photo.Title}"), Times.Once);
        });
    }
}