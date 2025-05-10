using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Models
{
    public class PaginationModel
    {
        public int PageSize { get; set; }

        public int Page { get; set; } = 0;

        public void ToFirst()
        {
            Page = 0;
        }

        public void Next()
        {
            Page++;
        }
    }
}
