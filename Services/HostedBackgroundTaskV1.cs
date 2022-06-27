using BackgroundTask.Utils;

namespace BackgroundTask.Services
{
    public class HostedBackgroundTaskV1 : IHostedService
    {
        private readonly ILogger<HostedBackgroundTaskV1> _logger;
        private readonly SchedulerService _scheduler;

        public HostedBackgroundTaskV1(ILogger<HostedBackgroundTaskV1> logger, SchedulerService scheduler)
        {
            _logger = logger;
            _scheduler = scheduler;
        }

        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            _scheduler.IntervalInSeconds(-1, 0, 1, CallBack);

            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            _scheduler.StopTasks();

            return Task.CompletedTask;
        }

        private void CallBack()
        {
            _logger.LogInformation($"Background task running: - {DateTime.Now}");
        }
    }
}
