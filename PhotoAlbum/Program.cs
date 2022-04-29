using Microsoft.Extensions.DependencyInjection;
using PhotoAlbum.Services;
using PhotoAlbum.Wrappers;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IConsoleWrapper, ConsoleWrapper>()
    .AddSingleton<IApiClient, IApiClient>()
    .BuildServiceProvider();

var consoleService = serviceProvider.GetRequiredService<IConsoleService>();

consoleService.StartApplication();