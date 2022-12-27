namespace DumperApplicationCore
{
    abstract class DumperFactory
    {
        public abstract IThing Create();

        public static DateTime CreatedAt()
        {
            return DateTime.Now;
        }
    }
}
