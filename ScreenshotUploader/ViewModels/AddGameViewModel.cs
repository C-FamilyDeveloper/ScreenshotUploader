using MVVMUtilities.Abstractions;
using MVVMUtilities.Core;
using ScreenshotUploader.Models;
using ScreenshotUploader.Services.Abstractions;

namespace ScreenshotUploader.ViewModels
{
    public class AddGameViewModel : BaseViewModel
    {
        private readonly IAppIdService appIdService;
        private readonly ISteamAPIService steamAPIService;
        private readonly INavigationService navigationService;
        private Game game = new();
        public Game Game
        {
            get { return game; }
            set { game = value; OnPropertyChanged(); }
        }

        public AsyncRelayCommand Confirm { get; set; }

        public AddGameViewModel(IAppIdService appIdService, ISteamAPIService steamAPIService, INavigationService navigationService)
        {
            this.appIdService = appIdService;
            this.steamAPIService = steamAPIService;
            this.navigationService = navigationService;
            Confirm = new(ConfirmAction);
        }

        public async Task ConfirmAction()
        {
            Game.AppId = appIdService.GetCorrectAppId(Game.AppId);
            Game.Name = await steamAPIService.GetGameNameByAppId(Game.AppId, CancellationToken.None);
            navigationService.ExecuteWithDispatcher<AddGameViewModel>(navigationService.Close<AddGameViewModel>);
        }
    }
}
