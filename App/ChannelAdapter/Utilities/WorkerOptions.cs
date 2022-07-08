namespace ChannelAdapter.Utilities
{
    public class WorkerOptions
    {
        public TimeSpan Interval { get; set; } = TimeSpan.FromSeconds(15);

        public string Source { get; set; } = string.Empty;

        public string DestinationComponent { get; set; } = string.Empty;

        public string DestinationTopic { get; set; } = string.Empty;

        public const string ConfigurationSectionName = "ChannelAdapter";
    }
}
