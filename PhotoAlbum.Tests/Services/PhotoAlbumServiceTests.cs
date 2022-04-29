using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoAlbum.Models;
using PhotoAlbum.Wrappers;

namespace PhotoAlbum.Tests;

[TestClass]
public class PhotoAlbumServiceTests
{
    private PhotoAlbumService _photoAlbumService;
    private Mock<IApiClient> _mockApiClient;

    [TestInitialize]
    public void BeforeEach()
    {
        _mockApiClient = new Mock<IApiClient>();
        _photoAlbumService = new PhotoAlbumService(_mockApiClient.Object);
    }

    [TestMethod]
    public void ShouldCallWriteLineWithHelloWorldValue()
    {
        var expectedUri = "https://jsonplaceholder.typicode.com/photos";
        _photoAlbumService.GetPhotos(1);
        _mockApiClient.Verify(x => x.GetAsync<List<Photos>>(expectedUri), Times.Once);
    }
}