using ScreenshotUploader.Builders.FileDestinationBuilder.Abstractions;
using ScreenshotUploader.Builders.FileDestinationBuilder.BuilderParts.Abstractions;
using ScreenshotUploader.Models;

namespace ScreenshotUploader.Builders.FileDestinationBuilder.Implementations
{
    public class FileQueryBuilder : IBuilder<FileQuery>,
        IFilePathsPart<FileQueryBuilder>,
        IAppIdPart<FileQueryBuilder>,
        IDestinationPart<FileQueryBuilder>
    {
        private IEnumerable<string> sourceFilePaths;
        private IEnumerable<string> destinations = [];
        private string appId;

        public FileQueryBuilder AddFilePaths(IEnumerable<string> sourceFilePaths)
        {
            this.sourceFilePaths = sourceFilePaths;
            return this;
        }

        public FileQueryBuilder AddFileGroupAppId(string appId)
        {
            this.appId = appId;
            return this;
        }

        public FileQueryBuilder AddDestination(string destinationPath)
        {
            destinations = destinations.Append(destinationPath);
            return this;
        }

        public FileQuery Build()
        {
            return new()
            {
                Destinations = destinations,
                AppId = appId,
                FilePaths = sourceFilePaths
            };
        }
    }
}
