using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Abstractions.DAL.BaseCRUD.AdditionalInterfaces
{
    public interface IExistable<T>
    {
        bool IsExist(T entity);
    }
}
