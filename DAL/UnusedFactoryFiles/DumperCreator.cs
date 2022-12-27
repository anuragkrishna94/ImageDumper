using DumperApplicationCore.Models;

namespace DumperApplicationCore.UnusedFactoryFiles
{
    internal class DumperCreator : DumperFactory
    {
        public override IThing Create()
        {
            return new Dumper();
        }
    }
}
