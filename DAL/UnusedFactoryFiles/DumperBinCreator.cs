using DumperApplicationCore.Models;

namespace DumperApplicationCore.UnusedFactoryFiles
{
    internal class DumperBinCreator : DumperFactory
    {
        public override IThing Create()
        {
            return new DumperBin();
        }
    }
}
