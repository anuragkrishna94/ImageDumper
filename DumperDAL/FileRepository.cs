using Microsoft.AspNetCore.Http;

namespace DumperDAL
{
    public class FileRepository
    {
        public async Task<List<string>> CopyFilesAsync(List<IFormFile> _objectFiles, string dumpLoc)
        {
            List<string> constructedFileNames = new();
            long size = _objectFiles.Sum(f => f.Length);
            int i = 0;
            foreach (var formFile in _objectFiles)
            {
                if (formFile.Length > 0)
                {
                    constructedFileNames.Add(Guid.NewGuid().ToString());
                    string filePath = Path.Combine(dumpLoc, constructedFileNames[i]);
                    ++i;
                    using var stream = File.Create(filePath);
                    await formFile.CopyToAsync(stream);
                }
            }
            return constructedFileNames;
        }
    }
}
