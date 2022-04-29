using System.Collections.Generic;
using PhotoAlbum.Models;

namespace PhotoAlbum.Tests.Helpers;

public class PhotoHelpers
{
    public static List<Photos> GetTestPhotoList() =>
        new List<Photos>(){
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
}