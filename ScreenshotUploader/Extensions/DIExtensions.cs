using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVVMUtilities.Abstractions;
using MVVMUtilities.Services;
using ScreenshotUploader.DAL.DataContext.Abstractions;
using ScreenshotUploader.Factories.Abstractions;
using ScreenshotUploader.Factories.Implementations;
using ScreenshotUploader.Models;
using ScreenshotUploader.Models.SteamModels.Responses;
using ScreenshotUploader.Services.Abstractions;
using ScreenshotUploader.Services.Abstractions.DAL;
using ScreenshotUploader.Services.Abstractions.Resources;
using ScreenshotUploader.Services.Abstractions.SteamAPI;
using ScreenshotUploader.Services.Implementations;
using ScreenshotUploader.Services.Implementations.DAL;
using ScreenshotUploader.ViewModels;
using ScreenshotUploader.Views;
using WinFormsUtilities.Services.Abstractions;
using WinFormsUtilities.Services.Implementations;

namespace ScreenshotUploader.Extensions
{
    public static class DIExtensions
    {
        public static void ConfigureMVVM(this IServiceCollection services)
        {
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<IMultiFileDialogService, 
                MultiSelectFileDialogService>((serviceProvider) =>
                {
                    return new MultiSelectFileDialogService("Изображения", "*.jpg");
                });
            services.AddSingleton<INavigationService, NavigationService>(serviceProvider =>
            {
                var navigationService = new NavigationService(serviceProvider);
                navigationService.ConfigureWindow<MainViewModel, MainView>();
                navigationService.ConfigureWindow<AddGameViewModel, AddGameView>();
                return navigationService;
            });
        }

        public static void SetupConfig(this IServiceCollection services) 
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json", optional: true, reloadOnChange: true)
                .Build();
            services.Configure<Config>(configuration);
        }

        public static void ConfigureBLLServices(this IServiceCollection services) 
        {
            services.AddSingleton<IFileContextFactory, FileContextFactory>();
            services.AddSingleton<ISteamDirectoryService, SteamDirectoryService>();
            services.AddSingleton<ISteamFacadeService, SteamFacadeService>();
            services.AddSingleton<IAppIdService, AppIdService>();
            services.AddSingleton<IFolderDialogService, FolderDialogService>();
            services.AddSingleton<ISearchRemoteFolderSteamService, SearchRemoteFolderSteamService>();
            services.AddSingleton<IFreeDateReservingService, FreeDateReservingService>();
            services.AddSingleton<IFileQueryService, FileQueryService>(); 
            services.AddSingleton<IScreenshotInfoService, ScreenshotInfoService>();
            services.AddScoped<ISteamAPIService, SteamAPIService>()
                .AddHttpClient();
            services.AddScoped<IRecentUsedGamesService, RecentUsedGameService>();
            services.AddSingleton<IScreenshotsFormingService, ScreenshotsFormingService>();
            services.AddSingleton<IResourcesService<Game>, GameResourcesService>();
            services.AddSingleton<IGameFormingService, GameFormingService>();
            services.AddScoped<IGameUsedFrequancyService, GameUsedFrequancyService>();
            services.AddScoped<IGameLastUsingDateTimeService, GameLastUsingDateTimeService>();
            services.AddScoped<IDateTimeStatisticsService, DateTimeStatisticsService>();
            services.AddScoped<IFrequancyStatisticsService, FrequancyStatisticsService>();
            services.AddSingleton<IScreenshotsStatisticsFacadeService, ScreenshotsStatisticsFacadeService>();
        }
    }
}
