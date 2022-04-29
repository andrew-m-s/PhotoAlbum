using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoAlbum.Models;
using PhotoAlbum.Wrappers;
using PhotoAlbumTests.Helpers;

namespace PhotoAlbum.Tests;

[TestClass]
public class PhotoAlbumServiceTests
{
    private PhotoAlbumService _photoAlbumService;
    private Mock<IApiClient> _mockApiClient;
    private List<Photos> _expectedPhotos;

    [TestInitialize]
    public void BeforeEach()
    {
        _expectedPhotos = PhotoHelpers.GetTestPhotoList();
        
        _mockApiClient = new Mock<IApiClient>();
        _mockApiClient
            .Setup(x => x.GetAsync<List<Photos>>(It.IsAny<string>()))
            .Returns(Task.FromResult(_expectedPhotos));
        _photoAlbumService = new PhotoAlbumService(_mockApiClient.Object);
    }

    [TestMethod]
    public void ShouldCallApiClientWithCorrectUri()
    {
        var expectedUri = "https://jsonplaceholder.typicode.com/photos";
        _photoAlbumService.GetPhotos(1);
        _mockApiClient.Verify(x => x.GetAsync<List<Photos>>(expectedUri), Times.Once);
    }

    [TestMethod]
    public void ShouldReturnValueFromApiClient()
    {
        var actualPhotos =_photoAlbumService.GetPhotos(1);

        Assert.AreEqual(_expectedPhotos, actualPhotos);
    }
}