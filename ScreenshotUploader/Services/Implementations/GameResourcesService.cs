using ScreenshotUploader.Extensions;
using ScreenshotUploader.Models;
using ScreenshotUploader.Models.SteamModels.Responses;
using ScreenshotUploader.Services.Abstractions.Resources;
using ScreenshotUploader.Specifications.Abstractions;


namespace ScreenshotUploader.Services.Implementations
{
    public class GameResourcesService : IResourcesService<Game>
    {
        private IEnumerable<Game> resources = [];

        public IEnumerable<Game> GetResourceBySpecification(ISpecification<Game> specification, PaginationModel paginationModel)
        {
            return resources.AsParallel().Where(specification.ToExpression().Compile())
                .Skip(paginationModel.PageSize * paginationModel.Page)
                .Take(paginationModel.PageSize);
        }

        public void SetResource(IEnumerable<Game> resource)
        {
            resources = resource;
        }
    }
}
