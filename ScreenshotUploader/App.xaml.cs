using Microsoft.Extensions.DependencyInjection;
using MVVMUtilities.Abstractions;
using System.Windows;
using ScreenshotUploader.ViewModels;
using ScreenshotUploader.Extensions;
using Microsoft.Extensions.Hosting;

namespace ScreenshotUploader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost host;

        public App()
        {
            host = new HostBuilder().ConfigureServices(services =>
            {
                services.SetupConfig();
                services.ConfigureMVVM();
                services.ConfigureBLLServices();
            }).Build();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            using var scope = host.Services.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<INavigationService>();
            service.Show<MainViewModel>();
        }
    }

}
