using DumperDAL;
using Microsoft.AspNetCore.Http;

namespace DumperApplicationCore.BusinessLogic
{
    public class DumpAndFetch
    {
        private readonly DumperDbContext _context;
        private readonly Repository _repository;
        public DumpAndFetch(DumperDbContext context)
        {
            _context = context;
            _repository = new(_context);
        }

        public string CreateDumper()
        {
            DumperDAL.Entities.Dumper entityDumper = new()
            {
                UniqueTitle = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                IsDestroyed = false
            };
            if (_repository.CreateDumper(entityDumper)) return entityDumper.UniqueTitle;
            return "";
        }

        public bool CheckIfDumperNameIsValid(string dumperName)
        {
            if(string.IsNullOrEmpty(dumperName)) return false;
            return _repository.GetDumperIDByTitle(dumperName) > 0;
        }

        public bool CreateDumperBin(string dumperBinTitle)
        {
            DumperDAL.Entities.DumperBin entityDumperBin = new()
            {
                DumperBinTitle = dumperBinTitle,
                CreatedAt = DateTime.Now
            };
            return _repository.CreateDumperBin(entityDumperBin);
        }

        public async Task ProcessObjectUploadAsync(List<IFormFile> imageFiles, string dumpLoc, string dumperName)
        {
            // Get Metadata and pass it on to the method below
            // Meanwhile, send processed byte stream and location to DAL
            // Class other than repository has to deal with this.

            FileRepository fileRepo = new();
            List<string> constructedFileNames = await fileRepo.CopyFilesAsync(imageFiles, dumpLoc);
            await InsertObjectMetaDataAsync(imageFiles, $"{dumperName}&|&0", constructedFileNames);
        }

        public async Task InsertObjectMetaDataAsync(List<IFormFile> imageFiles, string objContainerParam, List<string> constructedFileNames)
        {
            // Name
            // Extension
            // Object Type
            // Parent Dumper
            // Parent DumperBin

            // Insert these into DB using Repository class
            List<string> fileNames = new();
            for(int i = 0;i < imageFiles.Count && imageFiles[i].Length > 0;i++)
            {
                if (imageFiles[i].Length > 0)
                {
                    fileNames.Add(imageFiles[i].FileName);
                }
            }

            string[] containerParams = objContainerParam.Split("&|&");
            int dumperId = _repository.GetDumperIDByTitle(containerParams[0]);
            int dumperBinId = Convert.ToInt32(containerParams[1]);

            await _repository.InsertImageMetaDataAsync(fileNames, dumperId, dumperBinId, constructedFileNames);
        }
    }
}
