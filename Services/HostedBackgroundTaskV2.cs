namespace BackgroundTask.Services
{
    public class HostedBackgroundTaskV2 : IHostedService
    {
        private readonly ILogger<HostedBackgroundTaskV2> _logger;

        public HostedBackgroundTaskV2(ILogger<HostedBackgroundTaskV2> logger)
        {
            _logger = logger;
        }

        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background task starting!");
            _ = JobToDo(cancellationToken);

            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background task stopped!");

            return Task.CompletedTask;
        }

        private async Task JobToDo(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"Background task running: - {DateTime.Now}");

                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message, "Background task threw an exception");
                }

                await Task.Delay(2000, cancellationToken);
            }
        }
    }
}
