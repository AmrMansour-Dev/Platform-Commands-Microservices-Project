using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepository : IPlatformRepository
    {
        AppDbcontext _context;
        public PlatformRepository(AppDbcontext context)
        {
            _context = context;
        }

        public void CreatPlatform(Platform plat)
        {
            if(plat == null)
            {
                throw new ArgumentNullException(nameof(plat));
            }

            _context.Platforms.Add(plat);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Platform GetPlatformByID(int id)
        {
            return _context.Platforms.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
