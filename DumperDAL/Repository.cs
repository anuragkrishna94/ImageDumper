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

        public int GetDumperIDByTitle(string dumperTitle)
        {
            if(_context.Dumper.Any(x => x.UniqueTitle.Equals(dumperTitle) && !x.IsDestroyed))
                return _context.Dumper.Where(x => x.UniqueTitle.Equals(dumperTitle) && !x.IsDestroyed).Select(x => x.ID).FirstOrDefault();

            return -1;
        }

        public async Task<int> InsertImageMetaDataAsync(List<string> fileNames, int parentDumperId, int parentDumperBinId)
        {
            List<DumperObject> dumperObjects = new();
            for(int i = 0; i < fileNames.Count; i++)
            {
                dumperObjects.Add(new DumperObject()
                {
                    FileName = fileNames.ElementAt(i),
                    ParentDumperBinID = parentDumperBinId,
                    ParentDumperID = parentDumperId,
                });
            }
            _context.AddRange(dumperObjects);
            return await _context.SaveChangesAsync();
        }
    }
}
