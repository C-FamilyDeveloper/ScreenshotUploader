using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsUtilities.Services.Abstractions;

namespace WinFormsUtilities.Services.Implementations
{
    public class FolderDialogService : IFolderDialogService
    {
        private FolderBrowserDialog folderDialog;
        public FolderDialogService()
        {
            folderDialog = new FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.Desktop,
                Multiselect = false
            };
        }

        public string GetFolderPath()
        {
            folderDialog.ShowDialog();
            if (string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
            {
                throw new ArgumentException("Папка не выбрана");
            }
            return folderDialog.SelectedPath;
        }
    }
}
