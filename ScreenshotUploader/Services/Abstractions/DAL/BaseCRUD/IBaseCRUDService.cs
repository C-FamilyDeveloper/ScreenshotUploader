using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Abstractions.DAL.BaseCRUD
{
    public interface IBaseCRUDService<T> : 
        ICreatable<T>,
        IReadable<T>,
        IUpdatable<T>,
        IDeletable<T>
    {
    }
}
