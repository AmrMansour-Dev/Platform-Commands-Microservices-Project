using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepository
    {
        bool SaveChanges();
        Platform GetPlatformByID(int id);
        IEnumerable<Platform> GetAllPlatforms();
        void CreatPlatform(Platform plat);
    }
}
