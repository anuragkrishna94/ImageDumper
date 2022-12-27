namespace DumperWeb.Helper
{
    internal class FileProcessingHelper
    {
        private readonly List<IFormFile> _objectFiles;
        internal List<string> fileMetadata;
        internal FileProcessingHelper(List<IFormFile> objectFiles)
        {
            _objectFiles = objectFiles;
            fileMetadata = new();
        }

        internal FileStream[] GetFileStreams(string dumpLoc)
        {
            long size = _objectFiles.Sum(f => f.Length);
            FileStream[] objFileStreams = new FileStream[_objectFiles.Count];
            int i = 0;
            foreach (var formFile in _objectFiles)
            {
                if (formFile.Length > 0)
                {
                    fileMetadata.Add(formFile.FileName);
                    string filePath = Path.Combine(dumpLoc, $"{Guid.NewGuid()}.png");
                    objFileStreams[i] = File.Create(filePath);
                    ++i;
                }
            }
            return objFileStreams;
        }
    }
}
