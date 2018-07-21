namespace Dodo.RestaurantBoard.Domain.Services
{
    public interface IFileService
    {
        bool Exists(string path);
    }
}