using ChannelAdapter.Data;
using ChannelAdapter.Services;
using ChannelAdapter.Utilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EventDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetSection(WorkerOptions.ConfigurationSectionName).GetValue<string>("Source"),
        options => 
            options.EnableRetryOnFailure()));

builder.Services.AddDaprClient();
builder.Services.AddControllers();
builder.Services.AddHostedService<Worker>();

builder.Services.Configure<WorkerOptions>(
    builder.Configuration.GetSection(WorkerOptions.ConfigurationSectionName));

var app = builder.Build();
app.MapControllers();
app.Run();
