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
            Dumper dumper = null;
            if(_context.Dumper.Any(x => x.UniqueTitle.Equals(dumperTitle) && !x.IsDestroyed))
                dumper = _context.Dumper.First(x => x.UniqueTitle.Equals(dumperTitle) && !x.IsDestroyed);
            if(dumper == null) return -1;
            if ((DateTime.Now - dumper.CreatedAt).TotalMinutes > 59) return -1;
            return dumper.ID;
        }

        public async Task<int> InsertImageMetaDataAsync(List<string> fileNames, int parentDumperId, int parentDumperBinId, List<string> constructedFileNames)
        {
            List<DumperObject> dumperObjects = new();
            for(int i = 0; i < fileNames.Count; i++)
            {
                dumperObjects.Add(new DumperObject()
                {
                    FileName = fileNames.ElementAt(i),
                    ParentDumperBinID = parentDumperBinId,
                    ParentDumperID = parentDumperId,
                    ConstructedFileName = constructedFileNames.ElementAt(i)
                });
            }
            _context.AddRange(dumperObjects);
            return await _context.SaveChangesAsync();
        }
    }
}
