using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVVMUtilities.Abstractions;
using MVVMUtilities.Services;
using ScreenshotUploader.Models;
using ScreenshotUploader.Services.Abstractions;
using ScreenshotUploader.Services.Implementations;
using ScreenshotUploader.ViewModels;
using ScreenshotUploader.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            services.AddSingleton<ISteamDirectoryService, SteamDirectoryService>();
            services.AddSingleton<ISteamFacadeService, SteamFacadeService>();
            services.AddSingleton<IAppIdService, AppIdService>();
            services.AddSingleton<IFolderDialogService, FolderDialogService>();
            services.AddSingleton<ISearchRemoteFolderSteamService, SearchRemoteFolderSteamService>();
            services.AddSingleton<IFreeDateReservingService, FreeDateReservingService>();
            services.AddSingleton<IFileQueryService, FileQueryService>(); 
            services.AddSingleton<IScreenshotInfoService, ScreenshotInfoService>();
        }
    }
}
