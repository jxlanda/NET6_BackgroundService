namespace BackgroundTask.Services
{
    public class HostedBackgroundTaskV3 : BackgroundService
    {
        private readonly ILogger<HostedBackgroundTaskV3> _logger;

        public HostedBackgroundTaskV3(ILogger<HostedBackgroundTaskV3> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background task starting!");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"Background task running: - {DateTime.Now}");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, "Background task threw an exception");
                }

                await Task.Delay(2000, stoppingToken);
            }

            _logger.LogInformation("Background task stopped!");

        }
    }
}
