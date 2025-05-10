using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Specifications.Abstractions.CombinedSpecifications
{
    public class NotSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> specification;

        public NotSpecification(ISpecification<T> specification)
        {
            this.specification = specification;
        }

        public bool IsSatisfiedBy(T source)
        {
            return !specification.IsSatisfiedBy(source);
        }
    }
}
