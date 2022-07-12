using Azure.Storage.Blobs;
using MessageEndpoint.Models;
using MessageEndpoint.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace MessageEndpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        public CommandController(ILogger<CommandController> logger, BlobServiceClient blobServiceClient)
        {
            _logger = logger;
            _blobServiceClient = blobServiceClient;
        }

        [HttpPost("Record")]
        [CustomTopic("%MESSAGE_ENDPOINT_COMPONENT%", "%MESSAGE_ENDPOINT_TOPIC%")]
        public async Task<ActionResult> RecordCommand(Command command)
        {
            var commandJson = JsonSerializer.Serialize(command);
            var commandBytes = Encoding.UTF8.GetBytes(commandJson);
            using (var commandStream = new MemoryStream(commandBytes))
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient("command-archive");
                var blobClient = containerClient.GetBlobClient($"{command.CommandId}.json");
                await blobClient.UploadAsync(commandStream);
            }

            return Ok();
        }

        private readonly ILogger<CommandController> _logger;
        private readonly BlobServiceClient _blobServiceClient;
    }
}
