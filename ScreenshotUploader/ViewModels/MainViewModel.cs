using Microsoft.Extensions.Options;
using MVVMUtilities.Abstractions;
using MVVMUtilities.Core;
using ScreenshotUploader.Extensions;
using ScreenshotUploader.Models;
using ScreenshotUploader.Models.SteamModels.Responses;
using ScreenshotUploader.Services.Abstractions;
using ScreenshotUploader.Services.Abstractions.DAL.RecentUsedGames;
using ScreenshotUploader.Services.Abstractions.Resources;
using ScreenshotUploader.Services.Abstractions.SteamAPI;
using System.Collections.ObjectModel;
using System.Windows;
using WinFormsUtilities.Services.Abstractions;

namespace ScreenshotUploader.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public CustomObservableCollection<Screenshot> Screenshots { get; set; } = new();

        public RelayCommand SelectScreenshots {  get; set; }
        public RelayCommand Upload {  get; set; }
        public RelayCommand Close {  get; set; }
        public RelayCommand ChooseSteamRootDirectory { get; set; }
        public RelayCommand AddGame { get; set; }
        public AsyncRelayCommand Load {  get; set; }    

        private readonly IMultiFileDialogService multiFileDialogService;
        private readonly IDialogService dialogService;
        private readonly ISteamFacadeService steamService;
        private readonly IFolderDialogService folderDialogService;
        private readonly IOptions<Config> config;
        private readonly ISearchRemoteFolderSteamService searchRemoteFolderSteamService;
        private readonly IRecentUsedGamesService recentUsedGamesService;
        private readonly INavigationService navigationService;
        private readonly IScreenshotsFormingService screenshotsFormingService;
        private readonly IResourcesService<Game> resourcesService;
        private readonly ISteamAPIService steamAPIService;

        public ObservableCollection<Game> Games { get; set; }


        public ObservableCollection<string> GameNames 
        { 
            get => new(Games.Select(i => i.Name));
            set => GameNames = value;
        }

        private int index = -1;

        public int GameSelectedIndex
        {
            get { return index; }
            set { index = value; OnPropertyChanged(); }
        }



        private string? steamDirectory;
        public string? SteamDirectory
        {
            get { return steamDirectory; }
            set { steamDirectory = value; OnPropertyChanged(); }
        }


        public MainViewModel(IMultiFileDialogService multiFileDialogService,
            IDialogService dialogService,
            ISteamFacadeService steamService,
            IFolderDialogService folderDialogService,
            IOptions<Config> config,
            ISearchRemoteFolderSteamService searchRemoteFolderSteamService,
            IRecentUsedGamesService recentUsedGamesService,
            INavigationService navigationService,
            IScreenshotsFormingService screenshotsFormingService,
            IResourcesService<Game> resourcesService,
            ISteamAPIService steamAPIService)
        {
            SelectScreenshots = new (ChooseScreenshotsAction);
            Upload = new(UploadAction);
            Close = new(CloseAction);
            ChooseSteamRootDirectory = new(ChooseSteamRootDirectoryAction);
            SteamDirectory = config.Value.SteamFolder;
            AddGame = new(AddGameAction);
            this.multiFileDialogService = multiFileDialogService;
            this.dialogService = dialogService;
            this.steamService = steamService;
            this.folderDialogService = folderDialogService;
            this.config = config;
            this.searchRemoteFolderSteamService = searchRemoteFolderSteamService;
            this.recentUsedGamesService = recentUsedGamesService;
            this.navigationService = navigationService;
            this.screenshotsFormingService = screenshotsFormingService;
            this.resourcesService = resourcesService;
            this.steamAPIService = steamAPIService;
            Games = new(this.recentUsedGamesService.Read());
            Games.CollectionChanged += GamesCollectionChanged;
            Load = new(LoadAction);
        }

        private async Task LoadAction()
        {
            var result = await steamAPIService.GetGameListAsync(CancellationToken.None);
            resourcesService.SetResource(result);
        }

        private void GamesCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Games));
            OnPropertyChanged(nameof(GameNames));
        }

        private void ChooseSteamRootDirectoryAction()
        {
            try
            {                
                SteamDirectory = folderDialogService.GetFolderPath(); 
                config.Value.SteamFolder = SteamDirectory;
                config.Value.RemoteFolder = searchRemoteFolderSteamService
                    .GetRemoteFolderSteam(config.Value.SteamFolder);
            }
            catch (Exception ex)
            {
                dialogService.ShowErrorMessage(ex.Message, "Ошибка");
            }
        }

        private void CloseAction()
        {
            Environment.Exit(0);
        }

        private void UploadAction()
        {
            try
            {
                steamService.UploadScreenshots(config.Value.RemoteFolder, Screenshots);
                GameSelectedIndex = -1;
                dialogService.ShowMessage("Успешно завершено");
            }
            catch (Exception ex)
            {
                dialogService.ShowErrorMessage(ex.Message, "Ошибка");
            }      
        }

        private void ChooseScreenshotsAction()
        {
            try
            {
                var paths = multiFileDialogService.GetFilePaths();
                var game = GameSelectedIndex >= 0 ? Games[GameSelectedIndex] : null;
                Screenshots.AddRange(screenshotsFormingService.CreateScreenshots(paths, game));
                GameSelectedIndex = -1;
            }
            catch (Exception ex) 
            {
                dialogService.ShowErrorMessage(ex.Message, "Ошибка");
            }
        }

        private void AddGameAction()
        {
            try
            {
                navigationService.ShowDialog<AddGameViewModel>();
                var context = navigationService.GetDataContext<AddGameViewModel>();
                if (context.Game.IsEmpty())
                {
                    return;
                }
                recentUsedGamesService.Create(context.Game);
                Games.Add(context.Game);
                GameSelectedIndex = Games.Count - 1;
            }
            catch (Exception ex)
            {
                dialogService.ShowErrorMessage(ex.Message, "Ошибка");
            }
        }
    }
}
