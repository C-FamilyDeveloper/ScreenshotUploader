namespace ScreenshotUploader.Models
{
    public class FileQuery
    {
        public string AppId { get; set; }
        public IEnumerable<string> FilePaths { get; set; }
        public IEnumerable<string> Destinations { get; set; }
    }
}
