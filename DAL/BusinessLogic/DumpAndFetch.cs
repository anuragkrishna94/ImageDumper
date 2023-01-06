﻿using DumperDAL;
using DumperDAL.Entities;
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
            Dumper entityDumper = new()
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
            DumperBin entityDumperBin = new()
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

        public List<string> GetImagesOfDumper(string dumperName)
        {
            int dumperId = _repository.GetDumperIDByTitle(dumperName);
            if (dumperId > 0) return _repository.GetImagesNamesByDumperID(dumperId);
            return new List<string>();
        }

        public void UpdateExpiredDumpers()
        {
            List<DumperDAL.Entities.Dumper> dumpers = _repository.GetExpiredDumpers();
            for (int i = 0; i < dumpers.Count; i++) _repository.MarkExpiredDumperAsDestroyed(dumpers.ElementAt(i));
        }

        public List<int> GetExpiredDumper()
        {
            return _repository.GetExpiredDumpers().Select(x => x.ID).ToList();
        }

        public List<string> GetImageNamesByDumperID(int dumperId)
        {
            return _repository.GetImagesNamesByDumperID(dumperId);
        }

        public async Task DeleteExpiredDumperImagesAsync(List<int> expiredDumperIDs, string imageDumpLoc)
        {
            for (int i = 0; i < expiredDumperIDs.Count; i++)
            {
                List<string> imagesToBeDeleted = GetImageNamesByDumperID(expiredDumperIDs.ElementAt(i));
                await DeleteImagesAsync(imagesToBeDeleted, imageDumpLoc);
            }
        }

        private Task DeleteImagesAsync(List<string> imagesToBeDeleted, string imageDumpLoc)
        {
            return Task.Run(() =>
            {
                for (int j = 0; j < imagesToBeDeleted.Count; j++)
                {
                    File.Delete(Path.Combine(imageDumpLoc, imagesToBeDeleted.ElementAt(j)));
                }
            });
        }

        public async Task UpdateExpiredDumpersAsync(List<int> expiredDumperIDs)
        {
            for (int i = 0; i < expiredDumperIDs.Count; i++) await _repository.MarkExpiredDumperAsDestroyedAsync(expiredDumperIDs.ElementAt(i));
        }
    }
}
