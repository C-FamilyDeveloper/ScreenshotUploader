using ScreenshotUploader.Models;
using ScreenshotUploader.Specifications.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Abstractions.Resources
{
    public interface IResourceGet<T>
    {
        IEnumerable<T> GetResourceBySpecification(ISpecification<T> specification, PaginationModel paginationModel);
    }
}
