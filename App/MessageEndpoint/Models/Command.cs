namespace MessageEndpoint.Models
{
    public class Command
    {
        // Properties
        public Guid CommandId { get; set; } = Guid.NewGuid();
        public string CommandType { get; set; } = string.Empty;
        public string CommandData { get; set; } = string.Empty;
        public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset Scheduled { get; set; } = DateTimeOffset.Now;
    }
}
