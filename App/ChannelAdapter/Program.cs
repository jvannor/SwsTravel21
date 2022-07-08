using ChannelAdapter.Services;
using ChannelAdapter.Utilities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddHostedService<Worker>();
builder.Services.Configure<WorkerOptions>(
    builder.Configuration.GetSection(WorkerOptions.ConfigurationSectionName));

var app = builder.Build();
app.MapControllers();
app.Run();
