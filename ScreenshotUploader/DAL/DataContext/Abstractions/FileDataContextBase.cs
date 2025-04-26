using System.IO;
using System.Text.Json;

namespace ScreenshotUploader.DAL.DataContext.Abstractions
{
    public abstract class FileDataContextBase : IUnitOfWork
    {
        public FileDataContextBase()
        {
            foreach (var prop in this.GetType().GetProperties())
            {
                var fileName = $"{prop.Name}.dat";
                var loadMethod = this.GetType().GetMethod(nameof(LoadDataFromFile));
                var genericLoad = loadMethod.MakeGenericMethod(prop.PropertyType);
                var data = genericLoad.Invoke(this, [fileName]);
                prop.SetValue(this, data);
            }
        }

        public void SaveChanges()
        {
            foreach (var prop in this.GetType().GetProperties())
            {
                var fileName = $"{prop.Name}.dat";
                SaveDataToFile(fileName, prop.GetValue(this));
            }
        }

        public T LoadDataFromFile<T>(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return GetEmptyMass<T>();
            }
            return JsonSerializer.Deserialize<T>(File.ReadAllText(fileName));
        }

        public void SaveDataToFile<T>(string fileName, T data)
        {
            File.WriteAllText(fileName, JsonSerializer.Serialize(data));
        }

        private T GetEmptyMass<T>()
        {
            var genericType = typeof(T).GetGenericArguments().First();
            var method = typeof(Enumerable).GetMethod(nameof(Enumerable.Empty)).MakeGenericMethod(genericType);
            return (T)method.Invoke(null, null);
        }
    }
}
