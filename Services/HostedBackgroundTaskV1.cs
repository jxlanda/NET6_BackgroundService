namespace BackgroundTask.Services
{
    public class HostedBackgroundTaskV1 : IHostedService
    {
        private readonly ILogger<HostedBackgroundTaskV1> _logger;
        private Timer? _timer;

        public HostedBackgroundTaskV1(ILogger<HostedBackgroundTaskV1> logger)
        {
            _logger = logger;
        }

        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            TimeSpan delayTime = TimeSpan.Zero;
            TimeSpan intervalTime = TimeSpan.FromMinutes(1);
            _timer = new Timer(
                CallBack,
                null,
                delayTime,
                intervalTime
                );
            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void CallBack(object? state)
        {
            _logger.LogInformation("Background task running");
        }
    }
}
