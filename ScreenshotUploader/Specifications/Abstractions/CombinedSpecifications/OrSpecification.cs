using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Specifications.Abstractions.CombinedSpecifications
{
    public class OrSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> firstSpecification;
        private readonly ISpecification<T> secondSpecification;

        public OrSpecification(ISpecification<T> firstSpecification, ISpecification<T> secondSpecification)
        {
            this.firstSpecification = firstSpecification;
            this.secondSpecification = secondSpecification;
        }

        public bool IsSatisfiedBy(T source)
        {
            return firstSpecification.IsSatisfiedBy(source) || secondSpecification.IsSatisfiedBy(source);
        }
    }
}
