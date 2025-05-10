using MVVMUtilities.Abstractions;
using MVVMUtilities.Core;
using ScreenshotUploader.Models;
using ScreenshotUploader.Services.Abstractions;
using ScreenshotUploader.Services.Abstractions.Resources;
using ScreenshotUploader.Services.Abstractions.SteamAPI;
using ScreenshotUploader.Specifications.Implementations.SteamGame;

namespace ScreenshotUploader.ViewModels
{
    public class AddGameViewModel : BaseViewModel
    {
        private readonly IAppIdService appIdService;
        private readonly ISteamAPIService steamAPIService;
        private readonly INavigationService navigationService;
        private readonly IResourcesService<Game> resourcesService;

        public CustomObservableCollection<string> GameNames { get; set; } = new();
        private IEnumerable<Game> games = [];

        private string gameText;
        public string GameText
        {
            get { return gameText; }
            set { gameText = value; OnPropertyChanged(); pagination.ToFirst(); }
        }


        private Game game = new();
        public Game Game
        {
            get { return game; }
            set { game = value; OnPropertyChanged(); }
        }

        private PaginationModel pagination = new();

        public AsyncRelayCommand Confirm { get; set; }
        public AsyncRelayCommand Search { get; set; }

        public AddGameViewModel(IAppIdService appIdService,
            ISteamAPIService steamAPIService,
            INavigationService navigationService,
            IResourcesService<Game> resourcesService)
        {
            this.appIdService = appIdService;
            this.steamAPIService = steamAPIService;
            this.navigationService = navigationService;
            this.resourcesService = resourcesService;
            Confirm = new(ConfirmAction);
            Search = new(SearchAction);
            pagination.PageSize = 10;
        }

        public async Task ConfirmAction()
        {
            Game = resourcesService.GetResourceBySpecification
                (
                 new GameNameEqualSpecification(GameText), new PaginationModel { PageSize = 1 }
                ).First();
            /*Game.AppId = appIdService.GetCorrectAppId(Game.AppId);
            Game.Name = await steamAPIService.GetGameNameByAppIdAsync(Game.AppId, CancellationToken.None);*/
            navigationService.ExecuteWithDispatcher<AddGameViewModel>(navigationService.Close<AddGameViewModel>);
        }

        public async Task SearchAction()
        {
            var specification = new GameNameSpecification(GameText);
            games = resourcesService.GetResourceBySpecification(specification, pagination);
            GameNames = new(games.Select(i => i.Name));
            OnPropertyChanged(nameof(GameNames));
            pagination.Next();
        }
    }
}
