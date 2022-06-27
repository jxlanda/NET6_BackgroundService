namespace BackgroundTask.Utils
{
    public class SchedulerService
    {
        private readonly List<Timer> timers = new ();

        public SchedulerService() { }

        private void ScheduleTask(int hour, int min, double intervalInHour, Action task)
        {
            bool startNow = (hour < 0);

            if (hour < 0) { hour = 0; }
            if (min < 0) { min = 0; }
            if (intervalInHour < 0) { intervalInHour = 0; }

            DateTime now = DateTime.Now;
            DateTime firstRun = new(now.Year, now.Month, now.Day, hour, min, 0, 0);
            if (now > firstRun)
            {
                firstRun = firstRun.AddDays(1);
            }

            TimeSpan timeToGo = startNow? TimeSpan.Zero : firstRun - now;
            if (timeToGo <= TimeSpan.Zero)
            {
                timeToGo = TimeSpan.Zero;
            }

            Timer timer = new(x =>
            {
                task.Invoke();
            }, null, timeToGo, TimeSpan.FromHours(intervalInHour));

            timers.Add(timer);
        }

        public void StopTasks()
        {
            timers.ForEach(t => t.Dispose());
            timers.Clear();
        }

        public void IntervalInSeconds(int hour, int min, double interval, Action task)
        {
           interval /= 3600;
           ScheduleTask(hour, min, interval, task);
        }

        public void IntervalInMinutes(int hour, int min, double interval, Action task)
        {
            interval /= 60;
            ScheduleTask(hour, min, interval, task);
        }

        public void IntervalInHours(int hour, int min, double interval, Action task)
        {
            ScheduleTask(hour, min, interval, task);
        }

        public void IntervalInDays(int hour, int min, double interval, Action task)
        {
            ScheduleTask(hour, min, interval, task);
        }
    }
}
