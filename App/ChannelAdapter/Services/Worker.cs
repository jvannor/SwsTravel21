namespace ChannelAdapter.Services
{
    public class Worker : BackgroundService
    {
        // Methods
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Worker executing at {DateTimeOffset.Now}.");
                await Task.Delay(5000);
            }
        }

        // Fields
        private readonly ILogger<Worker> _logger;
    }
}