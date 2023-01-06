using DumperApplicationCore.BusinessLogic;
using DumperDAL;

namespace DumperDeleterService
{
    public class DumperDeleterWorker : BackgroundService
    {
        private readonly ILogger<DumperDeleterWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public DumperDeleterWorker(ILogger<DumperDeleterWorker> logger, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    DumpAndFetch _manager = scope.ServiceProvider.GetRequiredService<DumpAndFetch>();
                    //_manager.UpdateExpiredDumpers();
                    await ClearExpiredDumpersAync(_manager);
                }
                _logger.LogInformation("Expiration being applied");
                await Task.Delay(60_000, stoppingToken);
            }
        }

        /// <summary>
        /// Fetch exipred Dumper IDs
        /// async(await) delete dumper images
        /// async(await) update isDestroyed
        /// await Task.Delay
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        private async Task ClearExpiredDumpersAync(DumpAndFetch manager)
        {
            List<int> expiredDumperIDs = manager.GetExpiredDumper();
            if(expiredDumperIDs.Count > 0)
            {
                await manager.DeleteExpiredDumperImagesAsync(expiredDumperIDs, _configuration.GetValue<string>("ImageDumpLoc"));
                await manager.UpdateExpiredDumpersAsync(expiredDumperIDs);
            }
        }
    }
}