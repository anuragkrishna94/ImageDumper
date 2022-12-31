using DumperApplicationCore;
using DumperApplicationCore.BusinessLogic;

namespace DumperDeleterService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = @"Data Source=localhost;Initial Catalog=DumperApplicationDB;Integrated Security=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSqlServerConnection(connectionString);
                    services.AddHostedService<DumperDeleterWorker>();
                    services.AddScoped(typeof(DumpAndFetch));
                })
                .Build();

            host.Run();
        }
    }
}