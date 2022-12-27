using DumperDAL.Entities;

namespace DumperDAL
{
    public class Repository
    {
        private readonly DumperDbContext _context;
        public Repository(DumperDbContext context)
        {
            _context = context;
        }
        public bool CreateDumper(Dumper dumper)
        {
            if (dumper == null) return false;
            _context.Dumper.Add(dumper);
            return _context.SaveChanges() > 0;
        }

        public bool CreateDumperBin(DumperBin dumperBin)
        { 
            if(dumperBin == null || dumperBin.Dumper == null) return false;
            _context.DumperBin.Add(dumperBin);
            return _context.SaveChanges() > 0;
        }
    }
}
