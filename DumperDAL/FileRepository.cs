using Microsoft.AspNetCore.Http;

namespace DumperDAL
{
    public class FileRepository
    {
        public async Task CopyFilesAsync(List<IFormFile> _objectFiles, string dumpLoc)
        {
            long size = _objectFiles.Sum(f => f.Length);
            foreach (var formFile in _objectFiles)
            {
                if (formFile.Length > 0)
                {
                    string filePath = Path.Combine(dumpLoc, $"{Guid.NewGuid()}.png");

                    using var stream = File.Create(filePath);
                    await formFile.CopyToAsync(stream);
                }
            }
        }
    }
}
