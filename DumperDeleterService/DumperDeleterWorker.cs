using DumperApplicationCore.BusinessLogic;
using DumperDAL;

namespace DumperDeleterService
{
    public class DumperDeleterWorker : BackgroundService
    {
        private readonly ILogger<DumperDeleterWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        public DumperDeleterWorker(ILogger<DumperDeleterWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    DumpAndFetch _manager = scope.ServiceProvider.GetRequiredService<DumpAndFetch>();
                    _manager.UpdateExpiredDumpers();
                }
                _logger.LogInformation("Expiration being applied");
                await Task.Delay(60_000, stoppingToken);
            }
        }
    }
}