using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Builders
{
    public static class EnumerableBuilder
    {
        public static IEnumerable<T> Create<T>(int count) where T : new()
        {
            for (int i = 0; i < count; i++)
            {
                yield return new T();
            }
        }
    }
}
