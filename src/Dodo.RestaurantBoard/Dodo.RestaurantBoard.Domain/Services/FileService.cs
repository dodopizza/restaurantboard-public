using System.IO;

namespace Dodo.RestaurantBoard.Domain.Services
{
    public class FileService : IFileService
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}