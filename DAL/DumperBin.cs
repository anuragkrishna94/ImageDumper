namespace DAL
{
    internal class DumperBin : IThing
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
            return title;
        }

        public ICollection<string> ImageURLs { get; set; }
    }
}
