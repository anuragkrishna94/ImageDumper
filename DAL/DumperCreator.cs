namespace DumperApplicationCore
{
    internal class DumperCreator : DumperFactory
    {
        public override IThing Create()
        {
            return new Dumper();
        }
    }
}
