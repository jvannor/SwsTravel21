using Azure.Identity;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient();
builder.Services.AddControllers();
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.UseCredential(new DefaultAzureCredential());
    clientBuilder.AddBlobServiceClient(builder.Configuration.GetSection("Storage"));
});

var app = builder.Build();
app.UseCloudEvents();
app.MapSubscribeHandler();
app.MapControllers();

app.Run();
