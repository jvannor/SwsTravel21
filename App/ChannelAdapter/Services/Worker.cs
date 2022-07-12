using ChannelAdapter.Data;
using ChannelAdapter.Models;
using ChannelAdapter.Utilities;
using Dapr.Client;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace ChannelAdapter.Services
{
    public class Worker : BackgroundService
    {
        // Methods
        public Worker(ILogger<Worker> logger, IOptions<WorkerOptions> options, IServiceScopeFactory serviceScopeFactory, DaprClient daprClient)
        {
            _logger = logger;
            _options = options.Value;
            _serviceScopeFactory = serviceScopeFactory;
            _daprClient = daprClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker checking source for new events.");
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<EventDbContext>();
                    var events = dbContext.Events.Where(e => e.Completed == null).OrderBy(e => e.EventId);
                    foreach(var e in events)
                    {
                        _logger.LogInformation($"Worker processing event, \"{JsonSerializer.Serialize(e)}\".");
                        var command = new Command()
                        {
                            CommandType = e.EventCode,
                            CommandData = e.EventData ?? String.Empty,
                            Scheduled = e.Scheduled.ToUniversalTime()
                        };

                        await _daprClient.PublishEventAsync<Command>(_options.DestinationComponent, _options.DestinationTopic, command, stoppingToken);
                        e.Completed = DateTime.UtcNow;
                    }

                    dbContext.SaveChanges();
                }

                await Task.Delay(_options.Interval);
            }
        }

        // Fields
        private readonly ILogger<Worker> _logger;
        private readonly WorkerOptions _options;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly DaprClient _daprClient;
    }
}