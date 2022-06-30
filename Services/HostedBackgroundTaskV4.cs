namespace BackgroundTask.Services
{
    public class HostedBackgroundTaskV4 : BackgroundService
    {

        private readonly ILogger<HostedBackgroundTaskV4> _logger;
        private readonly PeriodicTimer _timer = new (TimeSpan.FromSeconds(1));

        public HostedBackgroundTaskV4(ILogger<HostedBackgroundTaskV4> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background task starting!");

            while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
            {
                await DoSomeWork();
            }

            _logger.LogInformation("Background task stopped!");

        }

        private async Task DoSomeWork()
        {
            try
            {
                _logger.LogInformation($"Background task running: - {DateTime.Now}");
                await Task.Delay(500);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Background task threw an exception");
            }
        }
    }
}
