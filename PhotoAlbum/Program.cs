using Microsoft.Extensions.DependencyInjection;
using PhotoAlbum.Services;
using PhotoAlbum.Wrappers;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IConsoleService, ConsoleService>()
    .AddSingleton<IConsoleWrapper, ConsoleWrapper>()
    .AddSingleton<IApiClient, ApiClient>()
    .AddSingleton<IPhotoAlbumService, PhotoAlbumService>()
    .BuildServiceProvider();

var consoleService = serviceProvider.GetRequiredService<IConsoleService>();

consoleService.StartApplication();