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
            .Returns("q");

        _mockPhotoAlbumService = new Mock<IPhotoAlbumService>();
        _mockPhotoAlbumService
            .Setup(x => x.GetPhotos(It.IsAny<int>()))
            .Returns(Task.FromResult(expectedPhotos));

        _consoleService = new ConsoleService(_mockConsoleWrapper.Object, _mockPhotoAlbumService.Object);

    }

    public void SetupSingleTestUserInput(string expectedUserInput) 
    {
        _mockConsoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns(expectedUserInput)
            .Returns("q");
    }

    [TestMethod]
    public void ShouldDisplayUserPromptOnStartup()
    {
        _consoleService.StartApplication();

        _mockConsoleWrapper.Verify(x => x.Write("Please enter an albumId, or q to exit: "));
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
        SetupSingleTestUserInput("2");

        _consoleService.StartApplication();

        _mockPhotoAlbumService.Verify(x => x.GetPhotos(2), Times.Once);
    }

    [TestMethod]
    public void ShouldDisplayErrorToUserIfInputIsNotIntegerAndNotQueryPhotos()
    {
        SetupSingleTestUserInput("NotAnInt");

        _consoleService.StartApplication();

        _mockConsoleWrapper.Verify(x => x.WriteLine("That Album ID was not valid, please try again!"));
        _mockPhotoAlbumService.Verify(x => x.GetPhotos(It.IsAny<int>()), Times.Never);
    }  

    [TestMethod]
    public void ShouldAllowUserToExitWithUpperCaseQInputWithoutQueryingAlbums()
    {
        SetupSingleTestUserInput("Q");

        _consoleService.StartApplication();

        _mockConsoleWrapper.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Never());
        _mockPhotoAlbumService.Verify(x => x.GetPhotos(It.IsAny<int>()), Times.Never);
    }

    [TestMethod]
    public void ShouldAllowUserToExitWithLowerCaseQInputWithoutQueryingAlbums()
    {
        SetupSingleTestUserInput("q");

        _consoleService.StartApplication();

        _mockConsoleWrapper.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Never());
        _mockPhotoAlbumService.Verify(x => x.GetPhotos(It.IsAny<int>()), Times.Never);
    }

    [TestMethod]
    public void ShouldDisplayQueriedAlbumIdToUser()
    {
        SetupSingleTestUserInput("2");

        _consoleService.StartApplication();

        _mockConsoleWrapper.Verify(x => x.WriteLine("photo-album 2"));
    }

    [TestMethod]
    public void ShouldDisplayPhotoIdsAndTitlesForQueriedAlbum()
    {
        SetupSingleTestUserInput("1");

        _consoleService.StartApplication();

        expectedPhotos.ForEach(photo =>
        {
            _mockConsoleWrapper.Verify(x => x.WriteLine($"[{photo.Id}] {photo.Title}"), Times.Once);
        });
    }

    [TestMethod]
    public void ShouldAllowUserToQueryAlbumsMultipleTimesBeforeExiting()
    {
        _mockConsoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("1")
            .Returns("2")
            .Returns("3")
            .Returns("q");

        _consoleService.StartApplication();

        _mockPhotoAlbumService.Verify(x => x.GetPhotos(1), Times.Once);
        _mockPhotoAlbumService.Verify(x => x.GetPhotos(2), Times.Once);
        _mockPhotoAlbumService.Verify(x => x.GetPhotos(3), Times.Once);
        
    }
}