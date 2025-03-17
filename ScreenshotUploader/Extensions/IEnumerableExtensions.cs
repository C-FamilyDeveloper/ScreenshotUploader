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
            return collection.Select(i =>
            {
                var propName = GetPropertyInfo(selector).Name;
                i.GetType().GetProperty(propName).SetValue(i, value);
                return i;
            });
        }
    }
}
