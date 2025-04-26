using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Extensions
{
    public static class IEnumerableExtensions
    {
        private static PropertyInfo GetPropertyInfo<T, U>(Expression<Func<T, U>> expression)
        {
            var body = expression.Body as MemberExpression;
            return body.Member as PropertyInfo;
        }

        public static IEnumerable<T> With<T, U>(this IEnumerable<T> collection, Expression<Func<T, U>> selector, U value)
        {
            var propName = GetPropertyInfo(selector).Name;
            return collection.Select(i =>
            {
                i.GetType().GetProperty(propName).SetValue(i, value);
                return i;
            });
        }

        public static IEnumerable<T> With<T, U>(this IEnumerable<T> collection, Expression<Func<T, U>> selector, IEnumerable<U> values)
        {
            if (collection.Count()!= values.Count())
            {
                throw new ArgumentException($"Wrong count! collection: {collection.Count()} values: {values.Count()}");
            }
            var propName = GetPropertyInfo(selector).Name;
            return collection.Zip(values, (i, j) =>
            {
                i.GetType().GetProperty(propName).SetValue(i, j);
                return i;
            });
        }
    }
}
