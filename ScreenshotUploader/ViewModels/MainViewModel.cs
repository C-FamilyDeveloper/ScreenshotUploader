using Microsoft.Extensions.Options;
using MVVMUtilities.Abstractions;
using MVVMUtilities.Core;
using ScreenshotUploader.Models;
using ScreenshotUploader.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsUtilities.Services.Abstractions;

namespace ScreenshotUploader.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private List<string> filePaths = new();

        public RelayCommand SelectScreenshots {  get; set; }
        public RelayCommand Upload {  get; set; }
        public RelayCommand Close {  get; set; }
        public RelayCommand ChooseSteamRootDirectory { get; set; }

        private readonly IMultiFileDialogService multiFileDialogService;
        private readonly IDialogService dialogService;
        private readonly ISteamFacadeService steamService;
        private readonly IFolderDialogService folderDialogService;
        private readonly IOptions<Config> config;
        private readonly ISearchRemoteFolderSteamService searchRemoteFolderSteamService;

        private string currentAppId;
        public string CurrentAppId
        {
            get { return currentAppId; }
            set { currentAppId = value.Trim(); OnPropertyChanged(); }
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
            ISearchRemoteFolderSteamService searchRemoteFolderSteamService)
        {
            SelectScreenshots = new (ChooseScreenshotsAction);
            Upload = new(UploadAction);
            Close = new(CloseAction);
            ChooseSteamRootDirectory = new(ChooseSteamRootDirectoryAction);
            SteamDirectory = config.Value.SteamFolder;
            this.multiFileDialogService = multiFileDialogService;
            this.dialogService = dialogService;
            this.steamService = steamService;
            this.folderDialogService = folderDialogService;
            this.config = config;
            this.searchRemoteFolderSteamService = searchRemoteFolderSteamService;
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
                steamService.UploadScreenshots(config.Value.RemoteFolder, filePaths, CurrentAppId);
                CurrentAppId = string.Empty;
                dialogService.ShowMessage("Успешно завершено");
            }
            catch (Exception ex)
            {
                dialogService.ShowErrorMessage(ex.Message, "Ошибка");
            }      
        }

        private void ChooseScreenshotsAction()
        {
            filePaths.Clear();
            try
            {
                var paths = multiFileDialogService.GetFilePaths();
                filePaths.AddRange(paths);
            }
            catch (Exception)
            {
                dialogService.ShowErrorMessage("Необходимо выбрать изображения", "Ошибка");
            }
        }
    }
}
