namespace DumperApplicationCore.Models
{
    internal class Dumper : IThing
    {
        private readonly string? _uniqueTitle;
        public string UniqueTitle
        {
            get { return _uniqueTitle; }
            private set
            {
                SetUniqueTitle();
            }
        }

        public string SetUniqueTitle(string title = "")
        {
            return Guid.NewGuid().ToString();
        }

        public ICollection<string> ImageURLs { get; set; }
        public ICollection<DumperBin> DumperBins { get; set; }
    }
}
