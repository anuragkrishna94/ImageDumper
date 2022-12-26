namespace DAL
{
    internal class DumperBinCreator : DumperFactory
    {
        public override IThing Create()
        {
            return new DumperBin();
        }
    }
}
