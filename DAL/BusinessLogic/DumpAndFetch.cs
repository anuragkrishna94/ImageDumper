using DumperDAL;

namespace DumperApplicationCore.BusinessLogic
{
    public class DumpAndFetch
    {
        private readonly DumperDbContext _context;
        public DumpAndFetch(DumperDbContext context)
        {
            _context = context;
        }

        public bool CreateDumper()
        {
            DumperDAL.Entities.Dumper entityDumper = new()
            {
                UniqueTitle = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                IsDestroyed = false
            };
            Repository repository = new(_context);
            return repository.CreateDumper(entityDumper);
        }

        public bool CreateDumperBin(string dumperBinTitle)
        {
            DumperDAL.Entities.DumperBin entityDumperBin = new()
            {
                DumperBinTitle = dumperBinTitle,
                CreatedAt = DateTime.Now
            };
            Repository repository = new(_context);
            return repository.CreateDumperBin(entityDumperBin);
        }
    }
}
