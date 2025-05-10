using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Specifications.Abstractions
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T source);
    }
}
