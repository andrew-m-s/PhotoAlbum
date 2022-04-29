using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoAlbum.Models;
using PhotoAlbum.Services;
using PhotoAlbum.Wrappers;

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

        _expectedPhotos = new List<Photos>(){
            new() {
                AlbumId = 1,
                Id = 1,
                Title = "Photo 1",
                URL = "https://via.placeholder.com/600/1",
                ThumbnailURL = "https://via.placeholder.com/150/1"
            },

            new() {
                AlbumId = 1,
                Id = 2,
                Title = "Photo 2",
                URL = "https://via.placeholder.com/600/2",
                ThumbnailURL = "https://via.placeholder.com/150/2"
            },

            new() {
                AlbumId = 2,
                Id = 3,
                Title = "Photo 3",
                URL = "https://via.placeholder.com/600/3",
                ThumbnailURL = "https://via.placeholder.com/150/3"
            }
        };

        _mockPhotoAlbumService = new Mock<IPhotoAlbumService>();
        _mockPhotoAlbumService.Setup(x => x.GetPhotos(1)).Returns(Task.FromResult(_expectedPhotos));

        _consoleService = new ConsoleService(_mockConsoleWrapper.Object, _mockPhotoAlbumService.Object);

    }

    [TestMethod]
    public void ShouldCallGetPhotos()
    {
        _consoleService.StartApplication();

        _mockPhotoAlbumService.Verify(x => x.GetPhotos(1), Times.Once);
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