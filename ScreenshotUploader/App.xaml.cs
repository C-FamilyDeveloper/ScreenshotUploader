using Microsoft.Extensions.DependencyInjection;
using MVVMUtilities.Abstractions;
using MVVMUtilities.Services;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;
using ScreenshotUploader.Views;
using ScreenshotUploader.ViewModels;
using ScreenshotUploader.Services.Abstractions;
using ScreenshotUploader.Services.Implementations;
using WinFormsUtilities.Services.Abstractions;
using WinFormsUtilities.Services.Implementations;
using Microsoft.Extensions.Configuration;
using ScreenshotUploader.Models;
using ScreenshotUploader.Extensions;

namespace ScreenshotUploader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.SetupConfig();
            services.ConfigureMVVM();
            services.ConfigureBLLServices();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var service = serviceProvider.GetRequiredService<INavigationService>();
            service.Show<MainViewModel>();
        }
    }

}
