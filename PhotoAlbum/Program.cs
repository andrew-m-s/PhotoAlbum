using Microsoft.Extensions.DependencyInjection;
using PhotoAlbum.Services;
using PhotoAlbum.Wrappers;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IConsoleWrapper, ConsoleWrapper>()
    .BuildServiceProvider();

var consoleService = serviceProvider.GetRequiredService<IConsoleService>();

consoleService.StartApplication();