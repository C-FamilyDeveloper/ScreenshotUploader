using ScreenshotUploader.Specifications.Abstractions;
using ScreenshotUploader.Specifications.Abstractions.CombinedSpecifications;
using System.Linq.Expressions;

namespace ScreenshotUploader.Extensions
{
    public static class SpecificationExtensions
    {
        public static ISpecification<T> Or<T>(this ISpecification<T> currentSpecification, ISpecification<T> otherSpecification)
        {
            return new OrSpecification<T>(currentSpecification, otherSpecification);
        }

        public static ISpecification<T> And<T>(this ISpecification<T> currentSpecification, ISpecification<T> otherSpecification)
        {
            return new AndSpecification<T>(currentSpecification, otherSpecification);
        }

        public static ISpecification<T> Not<T>(this ISpecification<T> specification)
        {
            return new NotSpecification<T>(specification);
        }

        public static Expression<Func<T, bool>> ToExpression<T>(this ISpecification<T> specification)
        {
            return (T t) => specification.IsSatisfiedBy(t);
        }
    }
}
