using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DumperDAL;

namespace DumperApplicationCore
{
    public static class CoreStartup
    {
        public static void AddSqlServerConnection(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DumperDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
